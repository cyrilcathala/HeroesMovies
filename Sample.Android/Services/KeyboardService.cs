using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Sample.Services;

namespace Sample.Droid.Services
{
    public class KeyboardService: Java.Lang.Object, IKeyboardService, ViewTreeObserver.IOnGlobalLayoutListener
    {
        public event EventHandler KeyboardDidShow;
        public event EventHandler KeyboardDidHide;
        public event EventHandler KeyboardWillShow;
        public event EventHandler KeyboardWillHide;

        private InputMethodManager inputMethodManager;

        private bool _wasShown = false;

        public KeyboardService()
        {
            GetInputMethodManager();
            SubscribeEvents();
        }

        public void OnGlobalLayout()
        {
            GetInputMethodManager();

            if (!_wasShown && IsCurrentlyShown())
            {
                KeyboardWillShow?.Invoke(this, EventArgs.Empty);
                KeyboardDidShow?.Invoke(this, EventArgs.Empty);
                _wasShown = true;
            }
            else if (_wasShown && !IsCurrentlyShown())
            {
                KeyboardWillHide?.Invoke(this, EventArgs.Empty);
                KeyboardDidHide?.Invoke(this, EventArgs.Empty);
                _wasShown = false;
            }
        }

        private bool IsCurrentlyShown()
        {
            return inputMethodManager.IsAcceptingText;
        }

        private void GetInputMethodManager()
        {
            if (inputMethodManager == null || inputMethodManager.Handle == IntPtr.Zero)
            {
                var activity = MainApplication.CurrentActivity;
                inputMethodManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            }
        }

        private void SubscribeEvents()
        {
            var activity = MainApplication.CurrentActivity;
            activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }
    }
}
