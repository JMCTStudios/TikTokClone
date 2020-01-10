using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFrontEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayer : ContentPage
    {
        public VideoPlayer()
        {
            InitializeComponent();
            // CrossMediaManager.Current.PlayFromAssembly("Mobile-FrontEnd.Resources.Videos.tiktok_video_1.mp4", typeof(VideoPlayer).Assembly);
            CrossMediaManager.Current.PlayFromResource("assets:///videos/tiktok-video-1.mp4");
        }
    }
}