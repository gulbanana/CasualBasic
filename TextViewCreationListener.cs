using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Casual_Basic
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("Basic")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class TextViewCreationListener : IWpfTextViewCreationListener
    {
        [Import]
        private IViewTagAggregatorFactoryService viewTagAggregatorFactoryService = null;

        [Export(typeof(AdornmentLayerDefinition))]
        [Name("Casual_Basic")]
        [Order(After = PredefinedAdornmentLayers.Text)]
        public AdornmentLayerDefinition adornmentLayer = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            var adorner = textView.GetAdornmentLayer("Casual_Basic");
            var aggregator = viewTagAggregatorFactoryService.CreateTagAggregator<VBKeywordTag>(textView);

            new CapsHider(textView, adorner, aggregator);
        }
    }
}
