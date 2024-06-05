namespace MIG.API
{
    public interface ILoadingScreenService : IService, IInitializableService
    {
        void ShowLoadingScreen();
        void HideLoadingScreen();
    }
}