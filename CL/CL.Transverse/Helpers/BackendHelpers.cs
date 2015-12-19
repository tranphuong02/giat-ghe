using System.Linq;
using CL.Transverse.Enums;

namespace CL.Transverse.Helpers
{
    public static class BackendHelpers
    {
        public static int GetResourceTypeFromFileName(string fileName)
        {
            var extension = fileName.Any(x => x == '.')
                ? fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - fileName.LastIndexOf('.') - 1).ToLower()
                : "";

            if (string.IsNullOrEmpty(extension))
            {
                return (int) ResourceType.Unknow;
            }

            if (Constants.FileExtension.Image.Any(x => x == extension))
            {
                return (int) ResourceType.Image;
            }

            if (Constants.FileExtension.Html.Any(x => x == extension))
            {
                return (int)ResourceType.Html;
            }

            if (Constants.FileExtension.Pdf.Any(x => x == extension))
            {
                return (int)ResourceType.Pdf;
            }

            if (Constants.FileExtension.Word.Any(x => x == extension))
            {
                return (int)ResourceType.Word;
            }

            if (Constants.FileExtension.Excel.Any(x => x == extension))
            {
                return (int)ResourceType.Excel;
            }

            return (int)ResourceType.Unknow;
        }
    }
}
