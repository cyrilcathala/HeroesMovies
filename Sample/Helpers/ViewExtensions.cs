using System;
using Xamarin.Forms;

namespace Sample.Helpers
{
    public static class ViewExtensions
    {
        public static T GetAncestor<T>(this Element view) where T : Element
        {
            var parent = view.Parent;
            while (parent != null)
            {
                if (parent is T ancestor)
                {
                    return ancestor;
                }

                parent = parent.Parent;
            }

            return default;
        }
    }
}
