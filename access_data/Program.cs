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
            // UpdateCategory(connection);
            // ListCategories(connection);
            // CreateCategory(connection);
            // CreateManyCategories(connection)
            // ExecuteProcedure(connection);
            // ExecuteReadProcedure(connection);
            // ExecuteScalar(connection);
            // ReadView(connection);
            // OneToOne(connection);
            // OneToMany(connection);
            // QueryMultiple(connection)
            // SelectIn(connection);
            // Like(connection)
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
    static void CreateManyCategories(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Title-1";
        category.Url = "url-1";
        category.Description = "Categoria-1";
        category.Order = 8;
        category.Summary = "Summary-1";
        category.Featured = false;

        var category2 = new Category();
        category2.Id = Guid.NewGuid();
        category2.Title = "Title-2";
        category2.Url = "url-2";
        category2.Description = "Categoria-2";
        category2.Order = 9;
        category2.Summary = "Summary-2";
        category2.Featured = false;

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

        var rows = connection.Execute(insertSql, new[]{
            new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            }, new
            {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }
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
    static void ExecuteProcedure(SqlConnection connection)
    {
        // var procedure = "EXEC [spDeleteStudent] @StudentId"; DESDE QUE O COMANDTYPE JA TAE SPECIFICADO NAO PRECISA EXEC
        var procedure = "[spDeleteStudent]";
        var obj = new { StudentId = "ID do estudante aqui" };
        var rows = connection.Execute(
            procedure,
            obj,
            commandType: System.Data.CommandType.StoredProcedure);
        Console.WriteLine($"{rows} registros afetados.");
    }
    static void ExecuteReadProcedure(SqlConnection connection)
    {
        // var procedure = "EXEC [spDeleteStudent] @StudentId"; DESDE QUE O COMANDTYPE JA TAE SPECIFICADO NAO PRECISA EXEC
        var procedure = "[spGetCoursesByCategory]";
        var parameter = new { CategoryId = "5baed79d-e717-9a35-8dc2-1a3500000000" };
        var courses = connection.Query(
            procedure,
            parameter,
            commandType: System.Data.CommandType.StoredProcedure);
        foreach (var course in courses)
        {
            Console.WriteLine(course.Id);
        }
    }
    static void ExecuteScalar(SqlConnection connection)
    {
        // GERAR ID PELO SQL E NAO NO CSHARP
        var category = new Category();
        category.Title = "Ammazon AWS";
        category.Url = "amazon";
        category.Description = "Categoria destinada a serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        // NAO CONCATENAR STRINGS - PARA EVITAR SQL INJECTION
        // USAR VARIAVEIS
        // SCOPE IDENTITY SO FUNCIONA SE TIVER IDENTITY SEED NOS CAMPOS, POR ISSO NAO USA GUID
        var insertSql = @"INSERT INTO
                    [Category]
                        OUTPUT inserted.[Id]
                VALUES(
                    NEWID(),
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

        var id = connection.ExecuteScalar<Guid>(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });
        Console.WriteLine($"A categoria inserida foi: {id}.");
    }
    static void ReadView(SqlConnection connection)
    {
        var sql = "SELECT * FROM [vwCourses]";
        var courses = connection.Query(sql);
        foreach (var item in courses)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }
    static void OneToOne(SqlConnection connection)
    {
        var sql = @"
        SELECT 
        * 
        FROM 
            [CareerItem]
        INNER JOIN
            [Course]
        ON
        [CareerItem].[CourseId] = [Course].[Id]";
        var items = connection.Query<CareerItem, Course, CareerItem>(
            sql,
            (careerItem, course) =>
            {
                careerItem.Course = course;
                return careerItem;
            }, splitOn: "Id");
        foreach (var item in items)
        {
            Console.WriteLine($"Titulo: {item.Title} - Curso: {item.Course?.Title}");
        }
    }
    static void OneToMany(SqlConnection connection)
    {
        var sql = @"
        SELECT 
            [Career].[Id], 
            [Career].[Title],
            [CareerItem].[CareerId], 
            [CareerItem].[Title] 
        FROM 
            [Career]
        INNER JOIN
            [CareerItem]
        ON
            [CareerItem].[CareerId] = [Career].[Id]
        ORDER BY 
            [Career].[Title] ";


        var careers = new List<Career>();

        var items = connection.Query<Career, CareerItem, Career>(
             sql,
             (career, item) =>
             {
                 var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                 if (car == null)
                 {
                     car = career;
                     car.Items.Add(item);
                     careers.Add(car);
                 }
                 else
                 {
                     car.Items.Add(item);
                 }
                 return career;
             }, splitOn: "CareerId"
         );
        foreach (var career in careers)
        {
            Console.WriteLine($"{career.Title}");
            foreach (var item in career.Items)
            {
                Console.WriteLine($"- {item.Title}");
            }
        }
    }
    static void QueryMultiple(SqlConnection connection)
    {
        var query = @"SELECT * FROM [Category];
                      SELECT * FROM [Course]";
        using (var multi = connection.QueryMultiple(query))
        {
            var categories = multi.Read<Category>();
            var courses = multi.Read<Course>();
            foreach (var item in categories)
            {
                Console.WriteLine(item.Title);
            }
            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);
            }
        };
    }
    static void SelectIn(SqlConnection connection)
    {
        var query = @"SELECT * FROM Career
                    WHERE [Id] IN @Id";

        connection.Query<Career>(query);

        var items = connection.Query<Career>(query, new
        {
            Id = new[]{
                "4327ac7e-4893-9f31-9a3b38a4e72b",
                "e6730d1c-6870-4df3-ae68-438624e04c72"
            }
        });

        foreach (var item in items)
        {
            Console.WriteLine(item.Title);
        }
    }
    static void Like(SqlConnection connection, string term = "api")
    {
        var query = @"SELECT * FROM [Course]
                     WHERE [Title] LIKE @exp";
        var items = connection.Query<Course>(query, new
        {
            exp = $"%{term}%"
        });
    }
    static void Transaction(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Minha categoria not save";
        category.Url = "minha-categoria";
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
        using (var transaction = connection.BeginTransaction())
        {
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            }, transaction);

            transaction.Commit();
            transaction.Rollback();
        }
    }
}
// install ado net 'dotnet add package Microsoft.Data.SqlClient'
// 5d1b6506-c980-8031-5957-26df00000000