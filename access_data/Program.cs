// See https://aka.ms/new-console-template for more information
using BaltaDataAcess.Model;
using Microsoft.Data.SqlClient;
using Dapper;


internal class Program
{
    private static void Main(string[] args)
    {
        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#t;TrustServerCertificate=True;Encrypt=True";

        using (var connection = new SqlConnection(connectionString))
        {
            UpdateCategory(connection);
            ListCategories(connection);
            // CreateCategory(connection);
        }
    }
    static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }
    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Ammazon AWS";
        category.Url = "amazon";
        category.Description = "Categoria destinada a serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        // NAO CONCATENAR STRINGS - PARA EVITAR SQL INJECTION
        // USAR VARIAVEIS
        var insertSql = @"INSERT INTO
                    [Category]
                VALUES(
                    @Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });
        Console.WriteLine($"{rows} linhas inseridas.");
    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("7dc10bdc-ee85-4179-8d02-652c586e8efb"),
            title = "Amazon AWS",
        });
        Console.WriteLine($"{rows} registros atualizados.");
    }
}

// install ado net 'dotnet add package Microsoft.Data.SqlClient'
