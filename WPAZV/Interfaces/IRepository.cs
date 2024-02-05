namespace WPAZV.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T value);
        void Edit(T value);
        void Delete(int value);
        List<T>? Get(string id = "0");
    }
}