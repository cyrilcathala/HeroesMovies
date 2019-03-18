using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Sample.Views
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
        }

		private void LoginButton_Clicked(object sender, EventArgs e)
		{
			ViewModel.Login();
		}
    }
}
