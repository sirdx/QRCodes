namespace QRCodes.Models;

public class QrCode
{
    public long QrCodeId { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}