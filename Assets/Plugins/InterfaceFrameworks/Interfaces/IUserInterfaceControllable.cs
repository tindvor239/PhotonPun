using System;

namespace SimpleUI.Interfaces
{
    public interface IUserInterfaceControllable
    {
        void Show(Action callback = null);
        void Hide(Action callback = null);
    }
}