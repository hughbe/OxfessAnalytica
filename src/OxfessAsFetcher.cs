using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Models;
using OxfessAnalytica.Common;

namespace OxfessAnalytica
{
    public class OxfessAsFetcher : OxfessFetcher
    {
        public async Task<IEnumerable<OxfessAsResult>> GetPosts()
        {
            IEnumerable<Post> posts = await GetAllPosts(since: new DateTime(2018, 12, 1));
            OxfessAsResult[] interestingPosts = posts
                .Where(IsPostInteresting)
                .Select(p => new OxfessAsResult(p))
                .ToArray();
            Console.WriteLine("Interesting Posts: " + interestingPosts.Length);
            return interestingPosts;
        }

        private static bool IsPostInteresting(Post post)
        {
            if (post.Message == null)
            {
                // No message - only an image. E.g. 117373032144331_238728210008812.
                return false;
            }

            if (post.Message.Contains("Colleges as", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (post.Message.Contains("Oxford Colleges and PPHs as", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}