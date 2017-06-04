namespace TheConference.Shared.Infrastructure.Data.EFCore
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
