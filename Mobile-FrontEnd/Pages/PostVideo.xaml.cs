using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using Mobile_FrontEnd.Models;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_FrontEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostVideo : ContentPage
    {
        public string VideoPostUrl { get; set; }
        public ObservableCollection<SettingItemModel> SettingItemModels = new ObservableCollection<SettingItemModel>();
        public PostVideo()
        {
            InitializeComponent();
            // InitListSetting();
            BindingContext = this;
        }

        private void InitListSetting()
        {
            this.SettingItemModels = new ObservableCollection<SettingItemModel>()
            {
                new SettingItemModel()
                {
                    IconPath = "",
                    Content = "Add location",
                    HasText = false,
                    HasIcon = true,
                    HasSwitch = false
                },
                new SettingItemModel()
                {
                    IconPath = "",
                    Content = "Who can view this video",
                    HasText = true,
                    HasIcon = true,
                    HasSwitch = false
                },
                new SettingItemModel()
                {
                    IconPath = "",
                    Content = "Allow comments",
                    HasIcon = false,
                    HasSwitch = true,
                    HasText = true
                },
                new SettingItemModel()
                {
                    IconPath = "",
                    Content = "Allow Duet and React",
                    HasSwitch = true,
                    HasIcon = false,
                    HasText = false
                },
                new SettingItemModel()
                {
                    IconPath = "",
                    Content = "Save to album",
                    HasSwitch = true,
                    HasIcon = false,
                    HasText = false
                }
            };
            SettingListView.ItemsSource = SettingItemModels;
        }

        public PostVideo(string videoFileUrl)
        {
            InitializeComponent();
            if (videoFileUrl != null)
            {
                this.VideoPostUrl = videoFileUrl;
            }
        }

        private async void OnSelectImageTapped(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            
            SelectedImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
        private void OnPostButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage(this.VideoPostUrl);
            foreach (var page in Navigation.ModalStack)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}