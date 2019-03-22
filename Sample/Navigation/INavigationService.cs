using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Navigation
{
    public interface INavigationService
    {
        void Initialize();
        Task ShowHome();
    }
}