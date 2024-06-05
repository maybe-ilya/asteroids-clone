using System;
using System.Threading.Tasks;

namespace MIG.API
{
    public interface ISceneLoader : IService
    {
        string CurrentSceneName { get; }
        Task LoadSceneAsync(string sceneName);
        Task LoadSceneAsync(int sceneBuildIndex);
        void LoadScene(string sceneName);
        void LoadScene(int sceneBuildIndex);
    }
}