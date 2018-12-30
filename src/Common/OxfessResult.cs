using System;
using Facebook.Models;

namespace OxfessAnalytica.Common
{
    public class OxfessResult
    {
        public string Id { get; }
        public DateTime Created { get; }
        public string OxfessId { get; }
        public string Message { get; }
        public string Permalink { get; }
        public int NumberOfReactions { get; set; }
        public int NumberOfComments { get; set; }

        public OxfessResult(Post post)
        {
            Id = post.Id;
            Created = post.Created;
            OxfessId = GetOxfessId(post);
            Message = post.Message;
            Permalink = post.Permalink;
            NumberOfReactions = post.Reactions.Summary.TotalCount;
            NumberOfComments = post.Comments.Summary.TotalCount;
        }
        
        private static string GetOxfessId(Post post)
        {
            // All posts should start with #OxfessNNNN.
            if (!post.Message.StartsWith("#Oxfess", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(post.Message);
            }

            return post.Message.Substring(0, post.Message.IndexOf("\n"));
        }
    }
}
