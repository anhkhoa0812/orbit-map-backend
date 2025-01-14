namespace OrbitMap.API.Payload.Request.Payment;

public class PayOSWebhookRequest
{
    public string Code { get; set; } // Mã trạng thái
    public string Desc { get; set; } // Mô tả trạng thái
    public bool Success { get; set; } // Trạng thái thành công hay thất bại
    public PayOSWebhookData Data { get; set; } // Dữ liệu chi tiết
    public string Signature { get; set; } // Chữ ký xác minh
}

public class PayOSWebhookData
{
    public int OrderCode { get; set; } // Mã đơn hàng
    public decimal Amount { get; set; } // Số tiền
    public string Description { get; set; } // Mô tả giao dịch
    public string AccountNumber { get; set; } // Số tài khoản người thanh toán
    public string Reference { get; set; } // Mã tham chiếu giao dịch
    public DateTime TransactionDateTime { get; set; } // Thời gian giao dịch
    public string Currency { get; set; } // Loại tiền tệ (VD: "VND")
    public string PaymentLinkId { get; set; } // ID liên kết thanh toán
    public string Code { get; set; } // Mã trạng thái giao dịch (VD: "00")
    public string Desc { get; set; } // Mô tả trạng thái giao dịch
    public string CounterAccountBankId { get; set; } // Mã ngân hàng đối ứng (nếu có)
    public string CounterAccountBankName { get; set; } // Tên ngân hàng đối ứng (nếu có)
    public string CounterAccountName { get; set; } // Tên tài khoản đối ứng (nếu có)
    public string CounterAccountNumber { get; set; } // Số tài khoản đối ứng (nếu có)
    public string VirtualAccountName { get; set; } // Tên tài khoản ảo (nếu có)
    public string VirtualAccountNumber { get; set; } // Số tài khoản ảo (nếu có)
}