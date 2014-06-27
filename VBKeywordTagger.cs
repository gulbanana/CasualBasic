using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;

namespace Casual_Basic
{
    internal sealed class VBKeywordTagger : ITagger<VBKeywordTag>
    {
        private readonly string[] _keywords = {"Imports", "New", "End", "Me", "As",
                                               "Public", "Private", "Protected", "Friend", 
                                               "Class", "Interface", "Module", "Namespace", "Sub", "Function", "Property",
                                               "ReadOnly", "Overrides", "MustOverride", "NotOverridable", "Optional",
                                               "If", "Else", "EndIf", "Do", "While", "Loop",
                                               "Get", "Set"};

        public VBKeywordTagger(ITextView textView)
        {
            textView.LayoutChanged += LayoutChanged;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<VBKeywordTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                var text = span.GetText();
                foreach (var k in _keywords)
                {
                    var ix = text.IndexOf(k);
                    if (ix != -1)
                    {
                        var start = span.Start.Add(ix);
                        var kwSpan = new SnapshotSpan(span.Snapshot, new Span(start.Position, k.Length));

                        yield return new TagSpan<VBKeywordTag>(kwSpan, new VBKeywordTag(k));
                    }
                }
            }

            yield break;
        }

        private void LayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            foreach (var span in e.NewOrReformattedSpans)
            {
                if (TagsChanged != null)
                {
                    TagsChanged(this, new SnapshotSpanEventArgs(span));
                }
            }
        }
    }
}
