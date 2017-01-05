using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

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
            return result;           
        }
    }
}
