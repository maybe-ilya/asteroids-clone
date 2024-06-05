using System;
using MIG.API;

namespace MIG.UI
{
    public interface IWindowFactory : IFactory<IWindow>
    {
        Type GetWindowType();
    }
}