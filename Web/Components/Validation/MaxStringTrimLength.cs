using System.ComponentModel.DataAnnotations;

namespace Web.Components.Validation
{
    public class MaxStringTrimLength : StringLengthAttribute
    {
        public MaxStringTrimLength(int maximumLength) : base(maximumLength)
        {
        }

        public override bool IsValid(object value)
        {
            if (value is string valueString)
            {
                return valueString.Trim().Length <= MaximumLength;
            }

            return false;
        }
    }
}