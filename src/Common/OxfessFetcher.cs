using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook;
using Facebook.Models;
using Facebook.Requests;
using Pagination.Primitives;

namespace OxfessAnalytica.Common
{
    public abstract class OxfessFetcher
    {
        protected GraphClient Client { get; } = new GraphClient(new Version(3, 2), "1649878385321071", "08fed8c7a764307fdb691fb40b48ce57");

        protected async Task<IEnumerable<Post>> GetAllPosts(DateTime? since)
        {
            Console.WriteLine("Loading posts...");
            Page page = await Client.GetPage<Page>(new PageRequest("oxfess"));
            PagedResponse<Post> posts = await Client.GetPosts(new PostsRequest(page.Id, PostsRequestEdge.Posts)
            {
                Since = since,
                Fields = new RequestField[]
                {
                    RequestField.Id,
                    RequestField.Message,
                    RequestField.Created,
                    RequestField.Permalink,
                    RequestField.Reactions.Summary(true),
                    RequestField.Comments.Summary(true)
                },
                PaginationLimit = 100
            });

            Post[] allPosts = posts.AllData().ToArray();
            Console.WriteLine($"Loaded {allPosts.Length} posts");
            return allPosts;
        }
    }
}