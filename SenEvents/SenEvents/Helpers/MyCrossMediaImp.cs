using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SenEvents
{
    class MyCrossMediaImp
    {
        public static async Task<ImageSource> PickImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //await DisplayAlert("Photos non supportées", ":( Permission not granted to photos.", "OK");
                throw new NotSupportedException("Permission not granted");
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });

            ImageSource img = null;

            if (file != null)
            {
                img = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }

            return await Task.FromResult(img);
        }

        public static async Task<ImageSource> TakeImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert("Photos non supportées", ":( Permission not granted to photos.", "OK");
                throw new NotSupportedException("Permission not granted or camera not available or take photo not supported");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92,
                SaveToAlbum = false,
                Directory = "Sample",
                Name = "test.jpg",
                AllowCropping = true,
                DefaultCamera = CameraDevice.Front
            });

            ImageSource img = null;

            if (file != null)
            {
                img = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }

            return await Task.FromResult(img);
        }

    }
}
