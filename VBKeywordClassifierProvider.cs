using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace Casual_Basic
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("Basic")]
    internal sealed class VBKeywordClassifierProvider : IClassifierProvider
    {
        [Import]
        private IClassificationTypeRegistryService classificationRegistry = null;

        public IClassifier GetClassifier(ITextBuffer textBuffer)
        {
            return textBuffer.Properties.GetOrCreateSingletonProperty(() => new VBKeywordClassifier(classificationRegistry.GetClassificationType(VBKeywordFormatDefinition.Name)));
        }
    }
}
