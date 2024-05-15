using Telerik.Windows.Documents.Model;

namespace CustomLayerAndCustomAnnotation
{
    public class CustomRangeEnd : AnnotationRangeEnd
    {
        public override bool SkipPositionBefore
        {
            get
            {
                return false;
            }
        }

        protected override AnnotationRangeStart CreateRangeStartInstance()
        {
            return new CustomRangeStart();
        }

        protected override DocumentElement CreateNewElementInstance()
        {
            return new CustomRangeEnd();
        }

        protected override void CopyContentFromOverride(DocumentElement fromElement)
        {
            CustomRangeEnd other = (CustomRangeEnd)fromElement;
            // Copy your custom properties from other to this here.
        }
    }
}
