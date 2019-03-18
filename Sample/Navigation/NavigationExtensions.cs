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
            if (navigation.NavigationStack.Any())
            {
                navigation.InsertPageBefore(page, navigation.NavigationStack.First());
            }
            else
            {
                await navigation.PushAsync(page);
            }

            await navigation.PopToRootAsync();
        }
    }
}
