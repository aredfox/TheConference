namespace TheConference.Shared.Infrastructure.Data.EFCore
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
