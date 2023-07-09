namespace _1CService.Application.Interfaces.Repositories
{
    public interface ILocalDatabaseGuidRole
    {
        void Add(string key, Guid value);
        Guid GetGuid(string key);
    }
}