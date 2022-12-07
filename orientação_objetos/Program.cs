using Balta.ContentContext;
using Balta.SubscriptionContext;

var articles = new List<Article>();


articles.Add(new Article("Article about OOP", "object-oriented"));
articles.Add(new Article("C# Heritance", "heritance-in-csharp"));
articles.Add(new Article("C# Polymorphism", "polymorphism-csharp"));

// foreach (var article in articles)
// {
//     Console.WriteLine(article.Id);
//     Console.WriteLine(article.Title);
//     Console.WriteLine(article.Url);
// }
var courses = new List<Course>();
var courseOOP = new Course("OOP", "fundamentals-oop");
var coursesCS = new Course("CSharp", "fundamentals-csharp");
var coursesASPNET = new Course("ASP.Net", "fundamentals-aspnet");

courses.Add(courseOOP);
courses.Add(coursesASPNET);
courses.Add(coursesCS);
var careers = new List<Career>();
var careerDotNet = new Career("Especialista .NET", "especialista-dotnet");
var careerItem2 = new CareerItem(2, "Learn OOP", "", courseOOP);
var careerItem1 = new CareerItem(1, "Getting Started", "", coursesCS);
var careerItem3 = new CareerItem(3, "Learn DotNet", "", coursesASPNET);

careerDotNet.Items.Add(careerItem2);
careerDotNet.Items.Add(careerItem3);
careerDotNet.Items.Add(careerItem1);
careers.Add(careerDotNet);

foreach (var career in careers)
{
    Console.WriteLine(career.Title);
    foreach (var item in career.Items.OrderBy(item => item.Order))
    {
        Console.WriteLine($"{item.Order} - {item.Title}");
        Console.WriteLine(item.Course?.Title);
        Console.WriteLine(item.Course?.Level);
        foreach (var notification in item.Notifications)
        {
            Console.WriteLine($"{notification.Property} - {notification.Message}");
        }
    }
}

var payPalSubscription = new PayPalSubscription();
var student = new Student();
student.CreateSubscription(payPalSubscription);