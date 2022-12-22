using Dapper.Contrib.Extensions;

namespace Blog.Repositories
{
    // T - Tipos  genericos, pois esse repositorio nao vai receber apenas <User>

    public class Repository<T> where T : class
    {
        // private readonly SqlConnection _connection;
        // public Repository(SqlConnection connection)
        //     => _connection = connection;
        public IEnumerable<T> Get()
        => Database.Connection.GetAll<T>();
        public T Get(int id)
            => Database.Connection.Get<T>(id);
        public long Create(T model)
        => Database.Connection.Insert<T>(model);

        public bool Update(T model)
            => Database.Connection.Update<T>(model);
        public bool Delete(T model)
                => Database.Connection.Delete<T>(model);
        public bool Delete(int id)
        {
            var model = Database.Connection.Get<T>(id);
            return Database.Connection.Delete<T>(model);
        }
    }
};