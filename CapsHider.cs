using System.Diagnostics;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows.Media;
using System.Windows.Controls;

namespace Casual_Basic
{
    public class CapsHider
    {
        private readonly string[] _keywords = { "Public", "Private", "Protected", "Friend", "Class", "Interface", "Module", "Namespace", "Sub", "Function", "End" };
        
        private readonly IWpfTextView _buffer;
        private readonly IAdornmentLayer _layer;

        private readonly Brush _foreground;
        private readonly Brush _background;
        private readonly double _pt;
        private readonly FontFamily _font;

        public CapsHider(IWpfTextView buffer, IAdornmentLayer layer)
        {
            _buffer = buffer;
            _layer = layer;

            _foreground = new SolidColorBrush(Colors.Blue);
            _background = new SolidColorBrush(Colors.White);
            _pt = 13.0;
            _font = new FontFamily("Consolas");

            _buffer.LayoutChanged += LayoutChanged;
        }

        private void LayoutChanged(object sender, TextViewLayoutChangedEventArgs args)
        {
            foreach (ITextViewLine line in args.NewOrReformattedLines)
            {
                var lineSpan = new SnapshotSpan(_buffer.TextSnapshot, Span.FromBounds(line.Start, line.End));

                foreach (var keyword in _keywords)
                {
                    var kwidx = lineSpan.GetText().IndexOf(keyword);
                    if (kwidx != -1)
                    {
                        var wordSpan = new SnapshotSpan(_buffer.TextSnapshot, Span.FromBounds(line.Start + kwidx, line.Start + kwidx + keyword.Length));

                        Geometry g = _buffer.TextViewLines.GetMarkerGeometry(wordSpan);
                        if (g != null)
                        {
                            var control = new TextBlock
                            {
                                Text = keyword.ToLower(),
                                Foreground = _foreground,
                                Background = _background,
                                FontSize = _pt,
                                FontFamily = _font
                            };

                            Canvas.SetLeft(control, g.Bounds.Left);
                            Canvas.SetTop(control, g.Bounds.Top);

                            _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, wordSpan, null, control, null);
                        }
                    }
                }
            }
        }
    }
}
