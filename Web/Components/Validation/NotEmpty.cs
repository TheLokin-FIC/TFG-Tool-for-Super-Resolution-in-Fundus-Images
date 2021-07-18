using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Web.Components.Validation
{
    public class NotEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}