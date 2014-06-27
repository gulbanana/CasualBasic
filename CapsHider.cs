using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Tagging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Casual_Basic
{
    internal sealed class CapsHider
    {
        private readonly string[] _keywords = { "Public", "Private", "Protected", "Friend", "Class", "Interface", "Module", "Namespace", "Sub", "Function", "End" };
        
        private readonly IWpfTextView _textView;
        private readonly IAdornmentLayer _adorner;
        private readonly ITagAggregator<ClassificationTag> _tagger;

        private readonly Brush _foreground;
        private readonly Brush _background;
        private readonly double _pt;
        private readonly FontFamily _font;

        public CapsHider(IWpfTextView textView, IAdornmentLayer adorner, ITagAggregator<ClassificationTag> tagger)
        {
            _textView = textView;
            _adorner = adorner;
            _tagger = tagger;

            _foreground = new SolidColorBrush(Colors.Blue);
            _background = new SolidColorBrush(Colors.Transparent);
            _pt = 13.0;
            _font = new FontFamily("Consolas");

            _textView.LayoutChanged += LayoutChanged;
        }

        private void LayoutChanged(object sender, TextViewLayoutChangedEventArgs args)
        {
            foreach (ITextViewLine line in args.NewOrReformattedLines)
            {
                foreach (var tag in _tagger.GetTags(line.Extent).Where(t => t.Tag.ClassificationType.Classification == "keyword"))
                {
                    AdornTag(tag);
                }
            }
        }

        private void AdornTag(IMappingTagSpan<ClassificationTag> tag)
        {
            foreach (var span in tag.Span.GetSpans(_textView.TextSnapshot))
            {
                Geometry g = _textView.TextViewLines.GetMarkerGeometry(span);
                if (g != null)
                {
                    var control = new TextBlock
                    {
                        Text = span.GetText().ToLower(),
                        Foreground = _foreground,
                        Background = _background,
                        FontSize = _pt,
                        FontFamily = _font
                    };

                    Canvas.SetLeft(control, g.Bounds.Left);
                    Canvas.SetTop(control, g.Bounds.Top);

                    _adorner.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, control, null);
                }
            }
        }
    }
}
