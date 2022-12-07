using Balta.SharedContext;

namespace Balta.ContentContext
{
    //Classe
    public abstract class Content : Base
    {
        //Construtor
        public Content(string title, string url)
        {
            Title = title;
            Url = url;
        }
        //propriedades
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}