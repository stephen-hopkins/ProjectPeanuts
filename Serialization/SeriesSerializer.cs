using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Windows.Storage;

namespace Peanuts.Serialization
{
    public static class SeriesSerializer
    {
        private static StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public static async void SerializeSeries(Series s)
        {
            StorageFile file = await localFolder.CreateFileAsync(s.ID.ToString(), CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Series));
                serializer.Serialize(stream.AsStreamForWrite(), s);
                await stream.FlushAsync();
                stream.Size = stream.Position;
            }
        }
    }
}
