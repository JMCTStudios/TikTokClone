using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Player;
using Mobile_FrontEnd.Models;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_FrontEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrePostVideo : ContentPage
    {
        private string CurrentVideoUrl { get; set; }
        public PrePostVideo()
        {
            InitializeComponent();
        }

        public PrePostVideo(string videoFileUrl)
        {
            InitializeComponent();
            if (videoFileUrl != null)
            {
                this.CurrentVideoUrl = videoFileUrl;
                CrossMediaManager.Current.Play(this.CurrentVideoUrl);
            }
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            
            // Application.Current.MainPage = new MainPage(this.CurrentVideoUrl);
            // foreach (var page in Navigation.ModalStack)
            // {
            //     Navigation.RemovePage(page);
            // }
            await Navigation.PushModalAsync(new PostVideo(this.CurrentVideoUrl));
        }

        private async void OnUploadVideoTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
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
                this.CurrentVideoUrl = fileUrl;
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

        private async void OnPauseAndPlayVideoTapped(object sender, EventArgs e)
        {
            switch (CrossMediaManager.Current.State)
            {
                case MediaPlayerState.Playing:
                    await CrossMediaManager.Current.Pause();
                    break;
                default:
                    await CrossMediaManager.Current.Play(CurrentVideoUrl);
                    break;
            }
        }
    }
}