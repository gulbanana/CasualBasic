using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casual_Basic
{
    internal sealed class VBKeywordClassifier : IClassifier
    {
        private readonly string[] _keywords = {"Imports", "Me", "As", "Of",
                                               "Public", "Private", "Protected", "Friend", "Inherits", "Implements",
                                               "Class", "Interface", "Module", "Namespace", "Sub", "Function", "Property",
                                               "ReadOnly", "Overrides", "MustOverride", "NotOverridable", "Optional", "ParamArray",
                                               "If", "Then", "Else", "EndIf", "Do", "While", "Loop", "Return",
                                               "Dim", "ReDim", "Get", "Set", "New", "End", 
                                               "False", "True", "Nothing"};
        private readonly IClassificationType _type;

        public VBKeywordClassifier(IClassificationType type)
        {
            if (type == null) throw new ArgumentNullException("type");
            _type = type; 
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var text = span.GetText();

            return (from k in _keywords
                    let ix = text.IndexOf(k)
                    where ix != -1
                    let start = span.Start.Add(ix)
                    let kwSpan = new SnapshotSpan(span.Snapshot, new Span(start.Position, k.Length))
                    select new ClassificationSpan(kwSpan, _type)).ToList();
        }

#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
}
