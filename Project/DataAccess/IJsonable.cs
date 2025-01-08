public interface IJsonable<T>
{
    public List<T> LoadAll();
    public  void WriteAll(List<T> models);
}