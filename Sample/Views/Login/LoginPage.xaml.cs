using System;
using Xamarin.Forms;

namespace Sample.Views
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Login();
        }
    }
}
