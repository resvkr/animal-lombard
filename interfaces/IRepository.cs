
namespace AnimalLombart.Repository.Interfaces;

public interface IRepository <T> where T : class{
    public void Save(T entity);
    public T FindById(string id);
    public List<T> FindAll();
    public void Delete(string id);
}