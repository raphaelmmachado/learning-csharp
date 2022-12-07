namespace Balta.ContentContext
{
    public class Career : Content
    {
        public Career(string title, string url)
         : base(title, url)
        {
            Items = new List<CareerItem>();
        }
        public IList<CareerItem> Items { set; get; }

        // TotalCouses não tem set pq nao faz sentido
        public int TotalCourses
        {
            get
            {
                return Items.Count;
            }
        }
    }

};