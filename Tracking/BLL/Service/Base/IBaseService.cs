namespace BLL.Service.Base;

public interface IBaseService<T>
{
    public Task<int> Create(T entity);

    public Task<IEnumerable<T>> GetAllModels();

    public Task<int> Update(T entity);

    public Task<bool> Delete(int id);
}