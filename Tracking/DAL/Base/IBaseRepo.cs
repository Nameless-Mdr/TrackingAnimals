namespace DAL.Base;

public interface IBaseRepo<T, E>
{
    public Task<T> Create(E entity);

    public Task<IEnumerable<E>> GetAllModels();

    public Task<T> Update(E entity);

    public Task<bool> Delete(T id);
}