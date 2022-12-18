using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository : Repository<User>
    {
        // private readonly SqlConnection Database.Connection;
        // public UserRepository(SqlConnection connection)
        //  : base(connection) => Database.Connection = connection;

        public List<User>? GetWithRoles()
        {
            var query = @"SELECT [User].*,
            [Role].* FROM [User]
            LEFT JOIN [UserRole] ON
            [UserRole].[UserId] = [User].[Id]
            LEFT JOIN [Role] ON
            [UserRole].[RoleId] = [Role].[Id]";

            var users = new List<User>();
            var items = Database.Connection.Query<User, Role, User>(
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;
                        if (role != null)
                            usr.Roles?.Add(role);
                        users.Add(usr);
                    }
                    else
                        usr.Roles?.Add(role);
                    return user;
                }, splitOn: "Id"
            );
            return users;
        }


    }
}