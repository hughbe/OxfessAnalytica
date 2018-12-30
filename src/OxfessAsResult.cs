using System;
using Facebook.Models;
using OxfessAnalytica.Common;

namespace OxfessAnalytica
{
    public class OxfessAsResult : OxfessResult
    {
        public string As { get; }

        public OxfessAsResult(Post post) : base(post)
        {
            As = GetAs(post);
        }

        private static string GetAs(Post post)
        {
            // All posts should start with #OxfessNNNN\n\n.
            int indexOfFirstNewline = post.Message.IndexOf('\n');
            int indexOfSecondNewLine = indexOfFirstNewline + 1;
            if (post.Message[indexOfSecondNewLine] != '\n')
            {
                throw new InvalidOperationException(post.Message);
            }

            int firstContentIndex = indexOfSecondNewLine + 1;
            int nextNewlineIndex = post.Message.IndexOf('\n', firstContentIndex);
            if (nextNewlineIndex == -1)
            {
                // No new lines, e.g. #Oxfess29590
                return post.Message.Substring(firstContentIndex).Trim();
            }

            string message = post.Message.Substring(firstContentIndex, nextNewlineIndex - firstContentIndex).Trim();
            if (message.EndsWith(":"))
            {
                return message.Substring(0, message.Length - 1);
            }

            return message;
        }
    }
}
