using System;

namespace JobBoard.Handlers
{
    public class FileHandler
    {
        public static string GetFileExtensionFromFileName(string fileName)
        {
            string fileExtension = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName) && fileName.Contains('.'))
                {
                    var splittedFileArray = fileName.Split('.');
                    fileExtension = splittedFileArray[splittedFileArray.Length - 1];
                }
            }
            catch (Exception)
            {
            }
            return fileExtension;
        }

  
    }
}
