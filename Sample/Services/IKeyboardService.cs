using System;
namespace Sample.Services
{
    public interface IKeyboardService
    {
        event EventHandler KeyboardDidShow;
        event EventHandler KeyboardDidHide;
        event EventHandler KeyboardWillShow;
        event EventHandler KeyboardWillHide;
    }
}
