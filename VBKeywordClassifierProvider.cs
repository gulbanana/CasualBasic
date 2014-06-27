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

        [Import]
        internal IClassifierAggregatorService classifierAggregator = null;

        internal static bool returningSelf = false;

        public IClassifier GetClassifier(ITextBuffer textBuffer)
        {
            if (returningSelf) return null;

            try
            {
                returningSelf = true;

                var innerClassifier = classifierAggregator.GetClassifier(textBuffer);
                var classificationType = classificationRegistry.GetClassificationType(VBKeywordFormatDefinition.Name);

                return textBuffer.Properties.GetOrCreateSingletonProperty(() => new VBKeywordClassifier(classificationType, innerClassifier));
            }
            finally
            {
                returningSelf = false;
            }
        }
    }
}
