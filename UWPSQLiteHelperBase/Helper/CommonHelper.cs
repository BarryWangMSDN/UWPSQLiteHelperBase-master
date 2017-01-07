using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace UWPSQLiteHelperBase.Helper
{
    public class CommonHelper
    {
        public async Task<string> ReadStringFromFile(StorageFile file)
        {
            IBuffer buffer = await FileIO.ReadBufferAsync(file);
            DataReader reader = DataReader.FromBuffer(buffer);
            byte[] fileContent = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(fileContent);
            var result = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
            //If I read file as text directly, I will get exception about encoding.Not so sure why this happens.
            return result;           
        }

        public async Task ShowMessage(string x)
        {
            MessageDialog dialog = new MessageDialog(x);
            await  dialog.ShowAsync();
        }
    }
}
