using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class NewsService : BaseService<NewsService>, INewsService
{
    private readonly IUploadService _uploadService;

    public NewsService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUploadService uploadService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _uploadService = uploadService;
    }

    public async Task<NewsResponse> CreateNewsAsync([FromForm] CreateNewsRequest request)
    {
        var news = _mapper.Map<News>(request);
        news.Id = Guid.NewGuid();
        news.UsefulReactionCount = 0;
        news.UselessReactionCount = 0;
        var uploadBusinessImageResult = await _uploadService.UploadImageAsync(request.BusinessImageFile);
        news.BusinessImage = uploadBusinessImageResult;
        if (request.Type.Equals(ENewsType.HeaderBanner))
        {
            if (request.BannerImageFile != null)
            {
                _logger.Error("Banner image is empty");
                throw new BadHttpRequestException("Banner image is required");
            }

            var uploadBannerImageResult = await _uploadService.UploadImageAsync(request.BannerImageFile!);
            news.BannerImage = uploadBannerImageResult;
        }

        if (request.NewsImageFiles != null && request.NewsImageFiles.Any())
        {
            var imageUrls = new List<string>();
            foreach (var newsImage in request.NewsImageFiles)
            {
                var uploadImageResult = await _uploadService.UploadImageAsync(newsImage);
                imageUrls.Add(uploadImageResult);
            }

            news.ImageUrls = imageUrls;
        }

        await _unitOfWork.GetRepository<News>().InsertAsync(news);
        var result = await _unitOfWork.CommitAsync() > 0;

        if (result == false)
        {
            _logger.Error("Failed to create news");
            throw new Exception("Failed to create news");
        }

        return _mapper.Map<NewsResponse>(news);
    }

    public async Task<List<NewsWithReactionResponse>> GetNewsAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Xác thực không thành công");
        var newsList = await _unitOfWork.GetRepository<News>().GetListAsync(
            selector: x => new News
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                ImageUrls = x.ImageUrls,
                BusinessName = x.BusinessName,
                BusinessAddress = x.BusinessAddress,
                BusinessImage = x.BusinessImage,
                UsefulReactionCount = x.UsefulReactionCount,
                UselessReactionCount = x.UselessReactionCount,
                Type = x.Type,
                ExpirationDate = x.ExpirationDate,
                BannerImage = x.BannerImage,
                CreatedDate = x.CreatedDate,
                LastModifiedDate = x.LastModifiedDate,
                NewsReactions = x.NewsReactions
            },
            predicate:
            x => x.ExpirationDate >= DateTime.UtcNow,
            include:
            x => x.Include(x => x.NewsReactions),
            orderBy: x => x.OrderBy(x => x.Type).ThenByDescending(x => x.CreatedDate)
        );
        var result = _mapper.Map<List<NewsWithReactionResponse>>(newsList);
        foreach (var news in newsList)
        {
            var matchingNews = result.FirstOrDefault(x => x.Id.Equals(news.Id));
            if (matchingNews == null) continue;

            var userReaction = news.NewsReactions.FirstOrDefault(x => x.Username.Equals(username));
            if (userReaction != null)
            {
                if (userReaction.ReactionType == EReactionType.Useful)
                {
                    matchingNews.IsUseful = true;
                }
                else if (userReaction.ReactionType == EReactionType.Useless)
                {
                    matchingNews.IsUseless = true;
                }
                else
                {
                    matchingNews.IsUseful = false;
                    matchingNews.IsUseless = false;
                }
            }
        }

        return result;
    }

    public async Task<NewsReactionResponse> ReactToNewsAsync(string username, Guid newsId, ReactNewsRequest request)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Xác thực không thành công");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (member == null)
            throw new AuthenticationException("Xác thực không thành công");
        var news = await _unitOfWork.GetRepository<News>().SingleOrDefaultAsync(
            predicate: x => x.Id == newsId && x.ExpirationDate >= DateTime.UtcNow
            // include: x => x.Include(x => x.NewsReactions)
            //     .ThenInclude(x => x.Member)
        );
        if (news == null)
            throw new Exception("Không tìm thấy News");
        var newsReaction = await _unitOfWork.GetRepository<NewsReaction>().SingleOrDefaultAsync(
            predicate: x => x.NewsId.Equals(newsId) && x.Username.Equals(username),
            include: x => x.Include(x => x.Member)
        );

        if (newsReaction != null)
        {
            if (string.IsNullOrEmpty(request.ReactionType.ToString()))
            {
                if (newsReaction.ReactionType == EReactionType.Useful)
                {
                    news.UsefulReactionCount--;
                }
                else if (newsReaction.ReactionType == EReactionType.Useless)
                {
                    news.UselessReactionCount--;
                }

                _unitOfWork.GetRepository<NewsReaction>().DeleteAsync(newsReaction);
                _unitOfWork.GetRepository<News>().UpdateAsync(news);
                var isSuccess = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccess)
                    throw new Exception("Failed to delete reaction");
                return _mapper.Map<NewsReactionResponse>(newsReaction);
            }

            if (request.ReactionType.Equals(newsReaction.ReactionType))
                throw new BadHttpRequestException("You have already reacted to this news");
            newsReaction.ReactionType = request.ReactionType!.Value;
            if (request.ReactionType == EReactionType.Useful)
            {
                news.UsefulReactionCount++;
                news.UselessReactionCount--;
            }
            else if (request.ReactionType == EReactionType.Useless)
            {
                news.UsefulReactionCount--;
                news.UselessReactionCount++;
            }

            _unitOfWork.GetRepository<NewsReaction>().UpdateAsync(newsReaction);
            _unitOfWork.GetRepository<News>().UpdateAsync(news);
            var isSuccessChange = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessChange)
                throw new Exception("Failed to change reaction");
            return _mapper.Map<NewsReactionResponse>(newsReaction);
        }
        else
        {
            if (string.IsNullOrEmpty(request.ReactionType.ToString()))
                throw new BadHttpRequestException("Reaction type is required");
            var newNewsReaction = new NewsReaction()
            {
                Id = Guid.NewGuid(),
                NewsId = news.Id,
                Username = username,
                MemberId = member.Id,
                ReactionType = request.ReactionType!.Value
            };
            if (request.ReactionType == EReactionType.Useful)
            {
                news.UsefulReactionCount++;
            }
            else if (request.ReactionType == EReactionType.Useless)
            {
                news.UselessReactionCount++;
            }

            await _unitOfWork.GetRepository<NewsReaction>().InsertAsync(newNewsReaction);
            _unitOfWork.GetRepository<News>().UpdateAsync(news);
            var isSuccess = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccess)
                throw new Exception("Failed to react to news");
            return _mapper.Map<NewsReactionResponse>(newNewsReaction);
        }
    }
}