using System;
using System.Dynamic;
using System.Linq;

namespace MailMerge
{
    public class DynamicObjectGetMemberBinder : GetMemberBinder
    {
        public DynamicObjectGetMemberBinder(string name, bool ignoreCase)
            : base(name, ignoreCase)
        {
        }

        public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
        {
            return target;
        }
    }
}
