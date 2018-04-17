using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    public class ImageStore
    {
        public async void upload(string filePath,
            EventHandler<FileUploadResponse> completed,
            EventHandler<FileUploadResponse> error,
            EventHandler<FileUploadProgress> progress)
        {
            CrossFileUploader.Current.FileUploadCompleted += completed;
            CrossFileUploader.Current.FileUploadError += error;
            CrossFileUploader.Current.FileUploadProgress += progress;

            string url = string.Format(@"{0}/images", ServiceConstants.BASE_URL);
            await CrossFileUploader.Current.UploadFileAsync(url, new FilePathItem("image", filePath), new Dictionary<string, string>()
            {
                /*<HEADERS HERE>*/
            });
        }
    }
}
