namespace manpreetsingh.pro.Models.Domain;

public abstract class PublishableEntityBase : EntityBase
{
    public bool IsPublished { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
    public int SortOrder { get; set; }
}
