using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_FrontEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostVideo : ContentPage
    {
        public string VideoPostUrl { get; set; }
        public PostVideo()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public PostVideo(string videoFileUrl)
        {
            InitializeComponent();
            if (videoFileUrl != null)
            {
                this.VideoPostUrl = videoFileUrl;
            }
        }
    }
}