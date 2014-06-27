using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace Casual_Basic
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Name)]
    [Name(Name)]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class VBKeywordFormatDefinition : EditorFormatDefinition
    {
        public const string Name = "vbkeyword";

        [Export]
        [Name(Name)]
        [BaseDefinition("keyword")]
        internal static ClassificationTypeDefinition classificationType = null;

        public VBKeywordFormatDefinition()
        {
            DisplayName = "VB Keyword";
            ForegroundColor = Colors.Transparent;
        }
    }
}
