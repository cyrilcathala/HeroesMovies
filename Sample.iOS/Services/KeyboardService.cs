using System;
using Sample.Services;
using UIKit;

namespace Sample.iOS.Services
{
    public class KeyboardService: IKeyboardService
    {
        public event EventHandler KeyboardDidShow;
        public event EventHandler KeyboardDidHide;
        public event EventHandler KeyboardWillShow;
        public event EventHandler KeyboardWillHide;

        public KeyboardService()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UIKeyboard.Notifications.ObserveWillShow(OnKeyboardWillShow);
            UIKeyboard.Notifications.ObserveDidShow(OnKeyboardDidShow);
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardWillHide);
            UIKeyboard.Notifications.ObserveDidHide(OnKeyboardDidHide);
        }

        private void OnKeyboardWillShow(object sender, UIKeyboardEventArgs e)
        {
            KeyboardWillShow?.Invoke(this, EventArgs.Empty);
        }

        private void OnKeyboardDidShow(object sender, EventArgs e)
        {
            KeyboardDidShow?.Invoke(this, EventArgs.Empty);
        }

        private void OnKeyboardDidHide(object sender, EventArgs e)
        {
            KeyboardDidHide?.Invoke(this, EventArgs.Empty);
        }

        private void OnKeyboardWillHide(object sender, UIKeyboardEventArgs e)
        {
            KeyboardWillHide?.Invoke(this, EventArgs.Empty);
        }
    }
}
