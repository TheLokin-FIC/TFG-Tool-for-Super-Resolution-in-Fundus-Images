using System.ComponentModel.DataAnnotations;

namespace Web.Components.Validation
{
    public class MinStringTrimLength : MinLengthAttribute
    {
        public MinStringTrimLength(int length) : base(length)
        {
        }

        public override bool IsValid(object value)
        {
            if (value is string valueString)
            {
                return Length <= valueString.Trim().Length;
            }

            return false;
        }
    }
}