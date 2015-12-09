using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Starbucks.Services
{
    public class DataStorage
    {
        public void SaveGifFile(byte[] data, string name)
        {
            var id = string.Format("{0}.gif", name);
            SaveData(data, id);
        }

        public Task<byte[]> LoadGifFile(string name)
        {
            var id = string.Format("{0}.gif", name);
            return LoadData(id);
        }

        public Task DeleteGifFile(string name)
        {
            var id = string.Format("{0}.gif", name);
            return DeleteData(id);
        }


        private async void SaveData(byte[] data, string id)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file =
                    await
                        folder.CreateFileAsync(id, CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task<byte[]> LoadData(string id)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(id);

                if (file == null)
                    return null;

                byte[] data;
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    data = new byte[stream.Length];
                    await stream.ReadAsync(data, 0, data.Length);
                }

                return data;
            }
            catch (Exception)
            {
            }

            return null;
        }

        private async Task DeleteData(string id)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(id);

                if (file != null)
                    await file.DeleteAsync();
            }
            catch (Exception)
            {
            }
        }

    }
}
