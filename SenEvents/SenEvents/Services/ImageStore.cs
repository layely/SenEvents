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
        public async void upload(string filePath)
        {
            //CrossFileUploader.Current.FileUploadCompleted += Current_FileUploadCompleted;
            //CrossFileUploader.Current.FileUploadError += Current_FileUploadError;
            //CrossFileUploader.Current.FileUploadProgress += Current_FileUploadProgress;

            string url = string.Format(@"{0}/images", ServiceConstants.BASE_URL);
            await CrossFileUploader.Current.UploadFileAsync(url, new FilePathItem("image", filePath), new Dictionary<string, string>()
            {
                /*<HEADERS HERE>*/
            });
        }

        //private void Current_FileUploadProgress(object sender, FileUploadProgress e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        progress.Progress = e.Percentage / 100.0f;
        //    });
        //}

        //private void Current_FileUploadError(object sender, FileUploadResponse e)
        //{
        //    isBusy = false;
        //    System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await DisplayAlert("File Upload", "Upload Failed", "Ok");
        //        progress.IsVisible = false;
        //        progress.Progress = 0.0f;
        //    });
        //}

        //private void Current_FileUploadCompleted(object sender, FileUploadResponse e)
        //{
        //    isBusy = false;
        //    System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await DisplayAlert("File Upload", "Upload Completed", "Ok");
        //        progress.IsVisible = false;
        //        progress.Progress = 0.0f;
        //    });
        //}

    }
}
