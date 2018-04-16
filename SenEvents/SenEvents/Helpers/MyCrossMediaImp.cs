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
        public class CMImage
        {
            public string Path { get; set; }
            public ImageSource ImageSource { get; set; }
        }

        public static async Task<CMImage> PickImage()
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

            CMImage cmImg = null;

            if (file != null)
            {
                cmImg = new CMImage
                {
                    ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    }),

                    Path = file.Path
                };
            }

            return await Task.FromResult(cmImg);
        }

        public static async Task<CMImage> TakeImage()
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

            CMImage cmImg = null;

            if (file != null)
            {
                cmImg = new CMImage
                {
                    ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    }),

                    Path = file.Path
                };
            }

            return await Task.FromResult(cmImg);
        }

    }
}
