using System;
using MIG.API;

namespace MIG.UI
{
    public interface IWindowController
    {
        Type GetWindowType();
        void SetWindow(IWindow window);
        void ClearWindow();
    }
}