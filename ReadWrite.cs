using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace orioks
{
    class ReadWrite
    {
        public static async Task saveStringToLocalFile(string filename, string content)
        {
            // saves the string 'content' to a file 'filename' in the app's local storage folder
            byte[] fileBytes = Encoding.UTF8.GetBytes(content.ToCharArray());

            // create a file with the given filename in the local folder; replace any existing file with the same name
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            // write the char array created from the content string into the file
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        public static async Task<string> readStringFromLocalFile(string filename)
        {
            // reads the contents of file 'filename' in the app's local storage folder and returns it as a string

            // access the local folder
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // open the file 'filename' for reading
            Stream stream = await local.OpenStreamForReadAsync(filename);
            string text;

            // copy the file contents into the string 'text'
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

    }
}
