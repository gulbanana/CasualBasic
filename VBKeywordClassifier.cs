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
        private readonly string[] _exclude = {"Object", "String", "Boolean", "Integer", "Long", "Short", "Byte", "Char", "Decimal", "Float", "Double"};

        private readonly IClassificationType _type;
        private readonly IClassifier _inner;

        public VBKeywordClassifier(IClassificationType type, IClassifier inner)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (inner == null) throw new ArgumentNullException("inner");

            _type = type;
            _inner = inner;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var preClassified = from cs in _inner.GetClassificationSpans(span)
                                where cs.ClassificationType.Classification == "keyword"
                                where !_exclude.Contains(cs.Span.GetText())
                                select new ClassificationSpan(cs.Span, _type);

            return preClassified.ToList();
        }

#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
}
