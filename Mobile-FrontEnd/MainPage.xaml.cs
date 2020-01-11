using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Player;
using Mobile_FrontEnd.Models;
using MobileFrontEnd.Pages;
using MobileFrontEnd.Pages.Popup;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Mobile_FrontEnd
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string currenVideoUrl { get; set; }
        public MainPage()
        {
            InitializeComponent();
            this.currenVideoUrl =
                "https://stream-video-data.herokuapp.com/video_streams/4d8621b4f7a396d65ffcbe8b8ceeb22c";
        }
        
        public IList<string> Mp4UrlList => new[]{
            "https://stream-video-data.herokuapp.com/video_streams/4d8621b4f7a396d65ffcbe8b8ceeb22c"
        };

        void playVideoMedia()
        {
            CrossMediaManager.Current.Play(currenVideoUrl);
            // CrossMediaManager.Current.Play();
        }

        async void OnPauseAndPlayVideoClicked(object sender, EventArgs args)
        {
            switch (CrossMediaManager.Current.State)
            {
                case MediaPlayerState.Playing:
                    await CrossMediaManager.Current.Pause();
                    break;
                // case MediaPlayerState.Paused:
                //     await CrossMediaManager.Current.Play(Mp4UrlList);
                //     break;
                // case MediaPlayerState.Stopped:
                //     await CrossMediaManager.Current.Play(Mp4UrlList);
                //     break;
                default:
                    await CrossMediaManager.Current.Play(currenVideoUrl);
                    break;
            }

            PlayAndPauseButton.IsVisible = true;
            await Task.Delay(1000);
            PlayAndPauseButton.IsVisible = false;
        }
        async void OnTappedShowCommentEvent(object sender, EventArgs args)
        {
            await Navigation.PushPopupAsync(new CommentHomePopup());
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            home_page.IsVisible = !home_page.IsVisible;
            if (home_page.IsVisible == false)
            {
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                await CrossMediaManager.Current.PlayPause();
            }
        }

        void OnMeTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //await Navigation.PushPopupAsync(new PersionalPopup());
            Navigation.PushAsync(new CoinManagement());
        }

        private async void OnTakeCameraClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
    
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions()
            // {
            //     Directory = "Videos",
            //     Name = "test.mp4"
            // });
            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            var fileInfoResponse = await CrossFileUploader.Current.UploadFileAsync(
                "https://stream-video-data.herokuapp.com/video_streams/upload", 
                    new FilePathItem("file", file.Path)
                );
            file.Dispose();
            var fileUrl = Deserialize(fileInfoResponse.Message).fileUrl;
            await DisplayAlert("File Url: ", fileUrl, "OK");
            if (fileInfoResponse.StatusCode == 200)
            {
                await CrossMediaManager.Current.Stop();
                await CrossMediaManager.Current.Play(fileUrl);
                // Set current file url
                this.currenVideoUrl = fileUrl;
            }
        }

        private UploadFileResponseModel Deserialize(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<UploadFileResponseModel>(jsonString, options);
        }
    }
}
