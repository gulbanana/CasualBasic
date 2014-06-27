using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Casual_Basic
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("Basic")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class CreationListener : IWpfTextViewCreationListener
    {
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("Casual_Basic")]
        [Order(After = PredefinedAdornmentLayers.Selection, Before = PredefinedAdornmentLayers.Text)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            new Casual_Basic(textView);
        }
    }
}
