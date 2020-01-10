using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Player;
using MobileFrontEnd.Pages;
using MobileFrontEnd.Pages.Popup;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Mobile_FrontEnd
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            playVideoMedia();
        }
        
        public IList<string> Mp4UrlList => new[]{
            "https://stream-video-data.herokuapp.com/video_streams/4d8621b4f7a396d65ffcbe8b8ceeb22c"
        };

        void playVideoMedia()
        {
            CrossMediaManager.Current.Play(Mp4UrlList);
            // CrossMediaManager.Current.PlayFromResource("assets:///videos/tiktok_video_1.mp4");
            // CrossMediaManager.Current.PlayFromAssembly("Mobile-FrontEnd.Resources.Videos.my_music.mp3", typeof(MainPage).Assembly);
            // CrossMediaManager.Current.PlayFromAssembly("Mobile-FrontEnd.Resources.Videos.tiktok_video_1.mp4", typeof(MainPage).Assembly);
            CrossMediaManager.Current.Play();
        }

        async void OnTappedPauseAndPlayVideo(object sender, EventArgs args)
        {
            switch (CrossMediaManager.Current.State)
            {
                case MediaPlayerState.Playing:
                    await CrossMediaManager.Current.Pause();
                    break;
                case MediaPlayerState.Paused:
                    await CrossMediaManager.Current.Play(Mp4UrlList);
                    break;
                case MediaPlayerState.Stopped:
                    await CrossMediaManager.Current.Play(Mp4UrlList);
                    break;
                default:
                    await CrossMediaManager.Current.Play(Mp4UrlList);
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
    }
}
