using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MIG.API;
using MIG.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIG.SceneLoading
{
    [UsedImplicitly]
    public sealed class SceneLoaderService : ISceneLoader, ISceneLoadNotifier
    {
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly ILogService _logger;
        private readonly SceneLoaderServiceSettings _settings;
        private bool _isCurrentlyLoading;

        private static CancellationToken ExitCancelToken => Application.exitCancellationToken;

        public SceneLoaderService(ILoadingScreenService loadingScreenService,
            ILogService logger,
            SceneLoaderServiceSettings settings)
        {
            _loadingScreenService = loadingScreenService;
            _settings = settings;
            _logger = logger;
        }

        public event Action BeforeSceneLoad;
        public event Action AfterSceneLoad;

        public string CurrentSceneName =>
            SceneManager.GetActiveScene().name;

        public async Task LoadSceneAsync(string sceneName)
            => await LoadSceneAsyncInternal(GetSceneLoadTask(sceneName), ExitCancelToken);

        public async Task LoadSceneAsync(int sceneBuildIndex)
            => await LoadSceneAsyncInternal(GetSceneLoadTask(sceneBuildIndex), ExitCancelToken);

        public void LoadScene(string sceneName)
            => LoadSceneAsync(sceneName).Forget();

        public void LoadScene(int sceneBuildIndex)
            => LoadSceneAsync(sceneBuildIndex).Forget();

        private async Task LoadSceneAsyncInternal(Task loadingSceneTask, CancellationToken cancelToken)
        {
            if (_isCurrentlyLoading)
            {
                throw new NotSupportedException($"Multiple scene load operations aren't supported");
            }

            _isCurrentlyLoading = true;
            try
            {
                BeforeSceneLoad?.Invoke();
                _loadingScreenService.ShowLoadingScreen();
                await Task.WhenAll(loadingSceneTask, GetEmulatedDelayTask(cancelToken));
                _loadingScreenService.HideLoadingScreen();
                AfterSceneLoad?.Invoke();
            }
            catch (OperationCanceledException)
            {
                _logger.Error("Scene loading cancelled");
            }
            finally
            {
                _isCurrentlyLoading = false;
            }
        }

        private Task GetSceneLoadTask(string sceneName)
            => GetSceneLoadTask(SceneManager.LoadSceneAsync(sceneName), ExitCancelToken);

        private Task GetSceneLoadTask(int sceneBuildIndex)
            => GetSceneLoadTask(SceneManager.LoadSceneAsync(sceneBuildIndex), ExitCancelToken);

        private Task GetSceneLoadTask(AsyncOperation loadOperation, CancellationToken cancelToken)
        {
            var completionSource = new TaskCompletionSource<object>();

            loadOperation.completed += _ =>
            {
                completionSource.SetResult(null);
                cancelToken.ThrowIfCancellationRequested();
            };

            return completionSource.Task;
        }

        private Task GetEmulatedDelayTask(CancellationToken cancelToken)
            => Task.Delay(TimeSpan.FromSeconds(_settings.EmulatedDelaySeconds), cancelToken);
    }
}