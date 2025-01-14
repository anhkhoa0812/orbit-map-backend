IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Group] (
    [Name] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY ([Name])
);
GO

CREATE TABLE [Role] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Connection] (
    [ConnectionId] nvarchar(450) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [GroupName] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Connection] PRIMARY KEY ([ConnectionId]),
    CONSTRAINT [FK_Connection_Group_GroupName] FOREIGN KEY ([GroupName]) REFERENCES [Group] ([Name]) ON DELETE CASCADE
);
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Username] varchar(50) NOT NULL,
    [DisplayName] nvarchar(255) NOT NULL,
    [PhoneNumber] varchar(50) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [AvatarUrl] nvarchar(max) NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [Discriminator] nvarchar(8) NOT NULL,
    [Birthday] date NULL,
    [IsPremium] bit NULL,
    [LastActive] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Friendship] (
    [Id] uniqueidentifier NOT NULL,
    [RequesterId] uniqueidentifier NOT NULL,
    [AddresseeId] uniqueidentifier NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Friendship] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Friendship_User_AddresseeId] FOREIGN KEY ([AddresseeId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Friendship_User_RequesterId] FOREIGN KEY ([RequesterId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [LastMessageChat] (
    [Id] uniqueidentifier NOT NULL,
    [SenderId] uniqueidentifier NOT NULL,
    [SenderUsername] nvarchar(max) NOT NULL,
    [RecipientId] uniqueidentifier NOT NULL,
    [RecipientUsername] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [MessageLastDate] datetime2 NOT NULL,
    [GroupName] nvarchar(max) NOT NULL,
    [IsRead] bit NOT NULL,
    CONSTRAINT [PK_LastMessageChat] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LastMessageChat_User_RecipientId] FOREIGN KEY ([RecipientId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LastMessageChat_User_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Message] (
    [Id] uniqueidentifier NOT NULL,
    [SenderId] uniqueidentifier NOT NULL,
    [SenderUsername] nvarchar(max) NOT NULL,
    [RecipientId] uniqueidentifier NOT NULL,
    [RecipientUsername] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [DateRead] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Message_User_RecipientId] FOREIGN KEY ([RecipientId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Message_User_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PlayerIds] (
    [Id] uniqueidentifier NOT NULL,
    [PlayerId] nvarchar(max) NOT NULL,
    [MemberId] uniqueidentifier NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PlayerIds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PlayerIds_User_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Story] (
    [Id] uniqueidentifier NOT NULL,
    [Content] nvarchar(255) NULL,
    [MediaUrl] nvarchar(max) NOT NULL,
    [Location] nvarchar(50) NOT NULL,
    [Weather] nvarchar(50) NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [IsDisabled] bit NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Story] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Story_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([Id], [Name])
VALUES ('3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1', N'Admin'),
('3fd223f6-3edd-4c87-888a-35defcff39e8', N'Business'),
('d1cd3eef-3318-48e3-99f7-31a938fbd021', N'Member');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'Birthday', N'CreatedDate', N'Discriminator', N'DisplayName', N'IsPremium', N'LastActive', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([Id], [AvatarUrl], [Birthday], [CreatedDate], [Discriminator], [DisplayName], [IsPremium], [LastActive], [LastModifiedDate], [PasswordHash], [PhoneNumber], [RoleId], [Username])
VALUES ('68c029f3-b49f-41da-864c-40299f71a956', N'https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png', '1999-01-01', '2025-01-12T00:09:08.5319480+07:00', N'Member', N'quan', CAST(1 AS bit), '2025-01-12T00:09:08.5319470+07:00', NULL, N'e24ih8ftxem8WzOQkrSS/q4n7Yv3+eGp9GlZThzEFcs=', '0399533724', 'd1cd3eef-3318-48e3-99f7-31a938fbd021', 'quan'),
('b1cc911f-7d57-4043-a716-c5249da61270', N'https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg', '1999-01-01', '2025-01-12T00:09:08.5319340+07:00', N'Member', N'Khoa Gió Tai', CAST(1 AS bit), '2025-01-12T00:09:08.5319070+07:00', NULL, N'v6plobem2ptzJLRd532mc835oAiq5JhrqBgHaCbjR+Y=', '0123456789', 'd1cd3eef-3318-48e3-99f7-31a938fbd021', 'khoa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'Birthday', N'CreatedDate', N'Discriminator', N'DisplayName', N'IsPremium', N'LastActive', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'CreatedDate', N'Discriminator', N'DisplayName', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([Id], [AvatarUrl], [CreatedDate], [Discriminator], [DisplayName], [LastModifiedDate], [PasswordHash], [PhoneNumber], [RoleId], [Username])
VALUES ('bcd34cfc-02e3-430c-93d1-a4943e10293a', N'https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg', '2025-01-12T00:09:08.5344350+07:00', N'User', N'admin', NULL, N'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', '8123456789', '3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1', 'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'CreatedDate', N'Discriminator', N'DisplayName', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'Birthday', N'CreatedDate', N'Discriminator', N'DisplayName', N'IsPremium', N'LastActive', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([Id], [AvatarUrl], [Birthday], [CreatedDate], [Discriminator], [DisplayName], [IsPremium], [LastActive], [LastModifiedDate], [PasswordHash], [PhoneNumber], [RoleId], [Username])
VALUES ('cacf40b2-772b-4c20-a0c9-cd7359353622', N'https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png', '1999-01-01', '2025-01-12T00:09:08.5319460+07:00', N'Member', N'Hoàng Gió Nhải', CAST(1 AS bit), '2025-01-12T00:09:08.5319450+07:00', NULL, N'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', '1234567890', 'd1cd3eef-3318-48e3-99f7-31a938fbd021', 'hoang');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AvatarUrl', N'Birthday', N'CreatedDate', N'Discriminator', N'DisplayName', N'IsPremium', N'LastActive', N'LastModifiedDate', N'PasswordHash', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AddresseeId', N'CreatedDate', N'LastModifiedDate', N'RequesterId', N'Status') AND [object_id] = OBJECT_ID(N'[Friendship]'))
    SET IDENTITY_INSERT [Friendship] ON;
INSERT INTO [Friendship] ([Id], [AddresseeId], [CreatedDate], [LastModifiedDate], [RequesterId], [Status])
VALUES ('7dc0741b-b5b4-4b3e-808d-1da4524169ed', 'cacf40b2-772b-4c20-a0c9-cd7359353622', '2025-01-12T00:09:08.5300120+07:00', NULL, 'b1cc911f-7d57-4043-a716-c5249da61270', N'Accepted');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AddresseeId', N'CreatedDate', N'LastModifiedDate', N'RequesterId', N'Status') AND [object_id] = OBJECT_ID(N'[Friendship]'))
    SET IDENTITY_INSERT [Friendship] OFF;
GO

CREATE INDEX [IX_Connection_GroupName] ON [Connection] ([GroupName]);
GO

CREATE INDEX [IX_Friendship_AddresseeId] ON [Friendship] ([AddresseeId]);
GO

CREATE INDEX [IX_Friendship_RequesterId] ON [Friendship] ([RequesterId]);
GO

CREATE INDEX [IX_LastMessageChat_RecipientId] ON [LastMessageChat] ([RecipientId]);
GO

CREATE INDEX [IX_LastMessageChat_SenderId] ON [LastMessageChat] ([SenderId]);
GO

CREATE INDEX [IX_Message_RecipientId] ON [Message] ([RecipientId]);
GO

CREATE INDEX [IX_Message_SenderId] ON [Message] ([SenderId]);
GO

CREATE INDEX [IX_PlayerIds_MemberId] ON [PlayerIds] ([MemberId]);
GO

CREATE INDEX [IX_Story_UserId] ON [Story] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_User_PhoneNumber] ON [User] ([PhoneNumber]);
GO

CREATE INDEX [IX_User_RoleId] ON [User] ([RoleId]);
GO

CREATE UNIQUE INDEX [IX_User_Username] ON [User] ([Username]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250111170908_Initial', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [ExpiredRankDate] datetime2 NULL;
GO

ALTER TABLE [Message] ADD [StoryId] uniqueidentifier NULL;
GO

UPDATE [Friendship] SET [CreatedDate] = '2025-01-12T13:11:15.2140130+07:00'
WHERE [Id] = '7dc0741b-b5b4-4b3e-808d-1da4524169ed';
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedDate] = '2025-01-12T13:11:15.2155590+07:00', [ExpiredRankDate] = NULL, [LastActive] = '2025-01-12T13:11:15.2155580+07:00'
WHERE [Id] = '68c029f3-b49f-41da-864c-40299f71a956';
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedDate] = '2025-01-12T13:11:15.2155440+07:00', [ExpiredRankDate] = NULL, [LastActive] = '2025-01-12T13:11:15.2155250+07:00'
WHERE [Id] = 'b1cc911f-7d57-4043-a716-c5249da61270';
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedDate] = '2025-01-12T13:11:15.2174340+07:00'
WHERE [Id] = 'bcd34cfc-02e3-430c-93d1-a4943e10293a';
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedDate] = '2025-01-12T13:11:15.2155570+07:00', [ExpiredRankDate] = NULL, [LastActive] = '2025-01-12T13:11:15.2155550+07:00'
WHERE [Id] = 'cacf40b2-772b-4c20-a0c9-cd7359353622';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Message_StoryId] ON [Message] ([StoryId]);
GO

ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_Story_StoryId] FOREIGN KEY ([StoryId]) REFERENCES [Story] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250112061115_Add_Data_User', N'8.0.6');
GO

COMMIT;
GO

