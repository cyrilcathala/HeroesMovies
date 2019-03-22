using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sample.Mvvm;
using Sample.Navigation;

namespace Sample.Views
{
    public class HomeViewModel: ViewModelBase
    {
        public ObservableCollection<string> Spotlights { get; private set; } = new ObservableCollection<string>();

        public HomeViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                Spotlights.Add("Hey");
            }
        }
    }
}
