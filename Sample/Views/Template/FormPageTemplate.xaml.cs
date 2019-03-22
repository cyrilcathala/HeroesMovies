using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sample.Helpers;
using Sample.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Sample.Views
{
    [ContentProperty(nameof(Content))]
    public partial class FormPageTemplate
    {
        public View Content
        {
            get => ContentContainer.Content;
            set => ContentContainer.Content = value;
        }

        public View Footer
        {
            get => FooterContainer.Content;
            set => FooterContainer.Content = value;
        }

        public bool ShouldKeyboardHideFooter { get; set; } = false;

        private readonly IKeyboardService _keyboardService;

        public FormPageTemplate()
        {
            InitializeComponent();

            _keyboardService = App.Services.GetRequiredService<IKeyboardService>();

            LayoutChanged += FormPageTemplate_LayoutChanged;
        }

        private void FormPageTemplate_LayoutChanged(object sender, EventArgs e)
        {
            LayoutChanged -= FormPageTemplate_LayoutChanged;

            var page = this.GetAncestor<Xamarin.Forms.Page>();

            if (ShouldKeyboardHideFooter)
            {
                page.Appearing += Page_Appearing;
                page.Disappearing += Page_Disappearing;

                _keyboardService.KeyboardWillShow += KeyboardService_KeyboardIsShown;
                _keyboardService.KeyboardWillHide += KeyboardService_KeyboardIsHidden;
            }
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            // Important to avoid memory leaks!!
            _keyboardService.KeyboardWillShow -= KeyboardService_KeyboardIsShown;
            _keyboardService.KeyboardWillHide -= KeyboardService_KeyboardIsHidden;
        }

        private void Page_Appearing(object sender, EventArgs e)
        {
            _keyboardService.KeyboardWillShow += KeyboardService_KeyboardIsShown;
            _keyboardService.KeyboardWillHide += KeyboardService_KeyboardIsHidden;
        }


        private void KeyboardService_KeyboardIsShown(object sender, EventArgs e)
        {
            FooterContainer.IsVisible = false;
        }

        private void KeyboardService_KeyboardIsHidden(object sender, EventArgs e)
        {
            FooterContainer.IsVisible = true;
        }
    }
}
