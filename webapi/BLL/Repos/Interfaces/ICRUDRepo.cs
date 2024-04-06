namespace webapi.BLL.Repos.Interfaces
{
    public interface ICRUDRepo<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
    }
}
