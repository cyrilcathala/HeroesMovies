using Sample.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Controls
{
    [ContentProperty("Child")]
    public partial class Loader : ContentView
    {
        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create(
                "IsBusy",
                typeof(bool),
                typeof(Loader),
                default(bool),
                propertyChanged: (control, oldvalue, newvalue) => ((Loader)control).IsBusyChanged());

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public static readonly BindableProperty LoaderViewModelProperty =
            BindableProperty.Create("LoaderViewModel",
                typeof(ILoaderViewModel),
                typeof(Loader),
                null,
                propertyChanged: (control, oldvalue, newvalue) => ((Loader)control).LoaderViewModelChanged());

        public ILoaderViewModel LoaderViewModel
        {
            get => (ILoaderViewModel)GetValue(LoaderViewModelProperty);
            set => SetValue(LoaderViewModelProperty, value);
        }

        public static readonly BindableProperty LoadingColorProperty =
            BindableProperty.Create(
                propertyName: "LoadingColor",
                returnType: typeof(Color),
                declaringType: typeof(Loader),
                defaultValue: Color.White,
                propertyChanged: LoadingColorPropertyChanged);

        private static void LoadingColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is Loader loader)) return;

            loader.Loading.Color = (Color)newvalue;
        }

        public static readonly BindableProperty MinimumHeightProperty = BindableProperty.Create("MinimumHeight", typeof(double), typeof(Loader), default(double));

        public double MinimumHeight
        {
            get => (double)GetValue(MinimumHeightProperty);
            set => SetValue(MinimumHeightProperty, value);
        }

        public Color LoadingColor
        {
            get => Loading.Color;
            set => Loading.Color = value;
        }

        public View Child
        {
            get => ContentContainer.Content;
            set => ContentContainer.Content = value;
        }

        public View EmptyView
        {
            get => EmptyContainer.Content;
            set => EmptyContainer.Content = value;
        }

        public View ErrorView
        {
            get => ErrorContainer.Content;
            set => ErrorContainer.Content = value;
        }

        public bool IsManualLoading { get; set; }

        public Loader()
        {
            InitializeComponent();

            LayoutChanged += Loader_LayoutChanged;
        }

        private void Loader_LayoutChanged(object sender, System.EventArgs e)
        {
            if (MinimumHeight <= 0 || Height <= 0) return;

            if (Height < MinimumHeight)
            {
                HeightRequest = MinimumHeight;
            }
        }

        private void LoaderViewModelChanged()
        {
            if (LoaderViewModel == null) return;

            LoaderViewModel.PropertyChanged += LoaderViewModel_PropertyChanged;

            StateChanged();
        }

        private void LoaderViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoaderViewModel.State))
            {
                StateChanged();
            }
        }

        private void StateChanged()
        {
            switch (LoaderViewModel.State)
            {
                case LoaderState.NotStarted:
                    break;

                case LoaderState.Loading:
                    ErrorContainer.IsVisible = false;
                    EmptyContainer.IsVisible = false;
                    ContentContainer.IsVisible = false;

                    if (!IsManualLoading) StartLoading();
                    break;

                case LoaderState.Completed:
                    ErrorContainer.IsVisible = false;
                    EmptyContainer.IsVisible = false;
                    ContentContainer.IsVisible = true;

                    if (!IsManualLoading) StopLoading();
                    break;

                case LoaderState.Faulted:
                    EmptyContainer.IsVisible = false;
                    ErrorContainer.IsVisible = true;
                    ContentContainer.IsVisible = false;

                    StopLoading();
                    break;

                case LoaderState.Empty:
                    ErrorContainer.IsVisible = false;
                    EmptyContainer.IsVisible = true;
                    ContentContainer.IsVisible = false;

                    StopLoading();
                    break;
            }
        }

        private void IsBusyChanged()
        {
            if (IsBusy)
            {
                ContentContainer.IsVisible = false;
                StartLoading();
            }
            else
            {
                ContentContainer.IsVisible = true;
                StopLoading();
            }
        }

        private Task StartLoading()
        {
            Loading.IsVisible = true;
            Loading.IsRunning = true;

            return Task.CompletedTask;
        }

        private async Task StopLoading()
        {
            Loading.IsVisible = false;
            Loading.IsRunning = false;

            await ContentContainer.FadeTo(1, 250);
        }
    }
}