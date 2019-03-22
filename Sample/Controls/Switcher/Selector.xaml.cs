using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Mvvm;
using Xamarin.Forms;

namespace Sample.Controls
{
    public partial class Selector
    {
        public event EventHandler SelectionChanged;

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex),
                typeof(int),
                typeof(Selector),
                default,
                propertyChanged: (control, _, __) => ((Selector)control).SelectedIndexChanged());

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public DataTemplate ItemTemplate { get; set; }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable<LabelViewModel>),
            typeof(Selector),
            default(IEnumerable<LabelViewModel>),
            propertyChanged: (control, oldvalue, newvalue) => ((Selector)control).ItemsSourceChanged());

        public IEnumerable<LabelViewModel> ItemsSource
        {
            get { return (IEnumerable<LabelViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Selector()
        {
            InitializeComponent();
        }

        private void ResetGridContainer()
        {
            for (int i = GridContainer.Children.Count - 2; i >= 0; i--)
            {
                GridContainer.Children.Remove(GridContainer.Children[i]);
            }

            GridContainer.ColumnDefinitions.Clear();
        }

        private void ItemsSourceChanged()
        {
            ResetGridContainer();

            if (ItemsSource == null) return;

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += Selector_Tapped;

            var options = ItemsSource.ToList();

            for (int i = 0; i < options.Count; i++)
            {
                var option = options[i];

                GridContainer.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

                var itemView = ItemTemplate.CreateContent() as View;
                var itemContentView = new ContentView { Content = itemView };
                itemContentView.BindingContext = option;
                itemContentView.GestureRecognizers.Add(tapGesture);

                SetColumn(itemContentView, i);
                GridContainer.Children.Insert(0, itemContentView);
            }

            LayoutChanged -= Grid_LayoutChanged;
            LayoutChanged += Grid_LayoutChanged;
        }

        private void Selector_Tapped(object sender, EventArgs e)
        {
            var label = sender as View;
            var position = GetColumn(label);

            SelectedIndex = position;
        }

        private void BackgroundCard_LayoutChanged(object sender, EventArgs e)
        {
            UpdateBackground();
        }

        private async void Grid_LayoutChanged(object sender, EventArgs e)
        {
            if (SelectedOption.Width < 0) return;

            LayoutChanged -= Grid_LayoutChanged;

            UpdateBackground();
            await SelectedIndexChanged();
        }

        private void UpdateBackground()
        {
            BackgroundCard.CornerRadius = (int)(BackgroundCard.Height / 2);
        }

        private async Task SelectedIndexChanged()
        {
            await AnimateSelectedIndexChanged();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private async Task AnimateSelectedIndexChanged()
        {
            if (ItemsSource == null) return;

            var options = ItemsSource.ToList();
            var option = options[SelectedIndex];

            var width = SelectedOption.Width;
            SelectedOption.TranslateTo(SelectedIndex * width, 0, 250, Easing.CubicInOut);

            await SelectedOptionLabel.FadeTo(0, 125);

            SelectedOptionLabel.Text = option.Label;
            await SelectedOptionLabel.FadeTo(1, 125);
        }
    }
}