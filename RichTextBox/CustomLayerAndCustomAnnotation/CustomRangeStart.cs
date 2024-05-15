using Telerik.Windows.Documents.Model;

namespace CustomLayerAndCustomAnnotation
{
    public class CustomRangeStart : AnnotationRangeStart
    {
        public override bool SkipPositionBefore
        {
            get
            {
                return true;
            }
        }

        protected override DocumentElement CreateNewElementInstance()
        {
            return new CustomRangeStart();
        }

        protected override void CopyContentFromOverride(DocumentElement fromElement)
        {
            CustomRangeStart other = (CustomRangeStart)fromElement;
            // Copy your custom properties from other to this here.
        }
    }
}
