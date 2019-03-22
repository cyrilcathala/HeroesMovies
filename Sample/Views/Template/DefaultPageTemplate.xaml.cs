using Xamarin.Forms;

namespace Sample.Views
{
    [ContentProperty(nameof(Content))]
    public partial class DefaultPageTemplate
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

        public DefaultPageTemplate()
        {
            InitializeComponent();
        }
    }
}
