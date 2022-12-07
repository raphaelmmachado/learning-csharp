using Balta.ContentContext.Enums;
using Balta.SharedContext;

namespace Balta.ContentContext
{
    public class Lecture : Base
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public int DurationInMinutes { get; set; }
        public EContentLevel Level { get; set; }

    }
}