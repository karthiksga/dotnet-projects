using HotChocolate.Data;

namespace GraphQLTest.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        [UseFiltering]
        [UseSorting]
        public IList<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public DateTime Posted { get; set; }
        public string Title { get; set; }
        public Blog Blog { get; set; }
    }
}
