namespace MIG.API
{
    public interface IGameStateService : IService
    {
        void Start();
        void Play();
        void Finish();
    }
}