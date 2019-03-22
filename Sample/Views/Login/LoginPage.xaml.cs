using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Views
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();

            FormSelector.SelectionChanged += FormSelector_SelectionChanged;
        }

        private async void FormSelector_SelectionChanged(object sender, EventArgs e)
        {
            var revealContainer = FormSelector.SelectedIndex == 0 ? LoginContainer : SignupContainer;
            var hideContainer = revealContainer == LoginContainer ? SignupContainer : LoginContainer;
            var direction = revealContainer == LoginContainer ? 1 : -1;

            await Task.WhenAll(hideContainer.FadeTo(0, 250),
                               hideContainer.TranslateTo(-direction * 100, 0, 250));
            hideContainer.IsVisible = false;

            revealContainer.Opacity = 0;
            revealContainer.IsVisible = true;
            revealContainer.TranslationX = 100 * direction;
            await Task.WhenAll(revealContainer.FadeTo(1, 250),
                               revealContainer.TranslateTo(0, 0, 250));

        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Login();
        }
    }
}
