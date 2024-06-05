using System;

namespace MIG.API
{
    public interface IMainMenuWindow : IWindow
    {
        event Action OnPlayGameClick;
        event Action OnQuitGameClick;
    }
}