namespace manpreetsingh.pro.Models.Domain;

public class UploadedAsset : EntityBase
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public long Length { get; set; }
    public string? AltText { get; set; }
    public string? Caption { get; set; }
    public string AssetKind { get; set; } = "image";
}
