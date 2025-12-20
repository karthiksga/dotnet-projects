using GraphQLTest.DataRepository;
using GraphQLTest.Models;
using System.Xml;

namespace GraphQLTest.Extensions
{
    public static class Seeder
    {
        public static async Task CheckAndSeedAsync(BlogsContext context)
        {
            if (await context.Database.EnsureCreatedAsync())
            {
                var client = new HttpClient();
                var jeremydoc = await client.GetStringAsync(
                    "https://blog.jeremylikness.com/blog/index.xml");
                var jeremydocParsed = ParseDoc(jeremydoc);
                context.Blogs.Add(jeremydocParsed);
                var geekdoc = await client.GetStringAsync(
                    "https://www.geektrainer.dev/index.xml");
                context.Blogs.Add(ParseDoc(geekdoc));
                await context.SaveChangesAsync();
            }
        }

        private static Blog ParseDoc(string doc)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(doc);
            var channel = xmlDoc.SelectSingleNode("//channel");
            var blog = new Blog
            {
                Id = 0,
                Name = channel.SelectSingleNode("title").InnerText,
                Url = channel.SelectSingleNode("link").InnerText,
                Posts = new List<Post>(),
            };
            var posts = channel.SelectNodes("item");
            foreach (XmlElement post in posts)
            {
                var postItem = new Post
                {
                    Id = 0,
                    Title = post.SelectSingleNode("title").InnerText,
                    Posted = DateTime.Parse(post.SelectSingleNode("pubDate").InnerText),
                    Blog = blog
                };
                blog.Posts.Add(postItem);
            }
            return blog;
        }
    }
}
