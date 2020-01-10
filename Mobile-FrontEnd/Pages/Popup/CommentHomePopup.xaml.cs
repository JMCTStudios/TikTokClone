using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFrontEnd.Pages.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentHomePopup : PopupPage
    {
        public CommentHomePopup()
        {
            InitializeComponent();
        }

        async void OnTappedCloseCommentPopup(object sender, EventArgs args)
        {
            await Navigation.PopPopupAsync();
        }
        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
            //return base.OnBackButtonPressed();
        }
    }
}