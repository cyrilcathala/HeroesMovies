using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace Sample.Navigation
{
    public static class NavigationExtensions
    {
        /// <summary>
        /// Pushs a page and clears the navigation stack
        /// </summary>
        /// <returns>The and clear async.</returns>
        /// <param name="navigation">Navigation.</param>
        /// <param name="page">Page.</param>
        public static async Task PushAndClearAsync(this INavigation navigation, Page page)
        {
            await navigation.PushAsync(page);

            foreach (var previousPage in navigation.NavigationStack)
            {
                if (previousPage != page)
                    navigation.RemovePage(previousPage);
            }
        }
    }
}
