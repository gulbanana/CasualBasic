using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Casual_Basic
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("Basic")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class TextViewCreationListener : IWpfTextViewCreationListener
    {
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("Casual_Basic")]
        [Order(After = PredefinedAdornmentLayers.Text)]
        public AdornmentLayerDefinition adornmentLayer = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            new CapsHider(textView, textView.GetAdornmentLayer("Casual_Basic"));
        }
    }
}
