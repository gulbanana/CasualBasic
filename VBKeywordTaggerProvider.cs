using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Casual_Basic
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("Basic")]
    [Order(Before = "default")]
    [TagType(typeof(VBKeywordTag))]
    internal sealed class VBKeywordTaggerProvider : IViewTaggerProvider
    {
        private readonly Dictionary<ITextView, VBKeywordTagger> taggers = new Dictionary<ITextView, VBKeywordTagger>();

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (textView == null || buffer == null) return null;

            if (buffer == textView.TextBuffer)
            {
                if (!taggers.ContainsKey(textView))
                {
                    taggers[textView] = new VBKeywordTagger(textView);
                }
                return taggers[textView] as ITagger<T>;
            }
            else
            {
                return null;
            }
        }
    }
}
