using Microsoft.VisualStudio.Text.Tagging;

namespace Casual_Basic
{
    internal sealed class VBKeywordTag : ITag
    {
        public readonly string Lowercase;

        public VBKeywordTag(string keyword)
        {
            Lowercase = keyword.ToLower();
        }
    }
}
