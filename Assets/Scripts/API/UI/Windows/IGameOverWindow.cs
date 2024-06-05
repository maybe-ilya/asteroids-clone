using System;

namespace MIG.API
{
    public interface IGameOverWindow : IWindow<GameOverWindowData>
    {
        event Action OnRetryClicked;
        event Action OnExitClicked;
    }
}