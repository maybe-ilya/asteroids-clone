using System;

namespace MIG.API
{
    public interface ISceneLoadNotifier : IService
    {
        event Action BeforeSceneLoad;
        event Action AfterSceneLoad;
    }
}