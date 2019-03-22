using System;
using Sample.iOS.Renderers;
using Sample.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPageBase), typeof(ContentPageBaseRenderer))]
namespace Sample.iOS.Renderers
{
    public class ContentPageBaseRenderer: PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewController?.NavigationController == null) return;

            ViewController.NavigationController.InteractivePopGestureRecognizer.Delegate = new SwipeBackGestureDelegate(ViewController.NavigationController);
            ViewController.NavigationController.InteractivePopGestureRecognizer.Enabled = true;
        }
    }

    public class SwipeBackGestureDelegate: UIGestureRecognizerDelegate
    {
        [Weak] private readonly UINavigationController _navController;

        public SwipeBackGestureDelegate(UINavigationController navController)
        {
            _navController = navController;
        }

        public override bool ShouldBegin(UIGestureRecognizer recognizer)
        {
            return recognizer is UIScreenEdgePanGestureRecognizer
                && _navController?.ViewControllers != null
                                  && _navController.ViewControllers.Length > 1;
        }
    }
}