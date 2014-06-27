using System.Diagnostics;
using Microsoft.VisualStudio.Text;

namespace Casual_Basic
{
    public class BufferEditLister
    {
        private readonly ITextBuffer _buffer;

        public BufferEditLister(ITextBuffer buffer)
        {
            _buffer = buffer;
            _buffer.Changed += BufferChanged;
        }

        private void BufferChanged(object sender, TextContentChangedEventArgs args)
        {
            foreach (var change in args.Changes)
            {
                Debug.WriteLine(change);
            }
        }
    }
}
