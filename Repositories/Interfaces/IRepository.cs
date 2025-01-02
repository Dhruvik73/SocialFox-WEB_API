
namespace Repositories.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetDataByField(string fieldName, string fieldValue);
    }
}
