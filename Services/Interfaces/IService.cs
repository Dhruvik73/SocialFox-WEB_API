
namespace Services.Interfaces
{
    public interface IService<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetDataByField(string fieldName, string fieldValue);
    }
}
