using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Web.Components.Data;

namespace Web.Components.Validation
{
    public class FileProcessed : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is FileData file)
            {
                return file.Percentage == 100;
            }
            else if (value is IList<FileData> listFiles)
            {
                return listFiles.All(x => x.Percentage == 100);
            }

            return false;
        }
    }
}