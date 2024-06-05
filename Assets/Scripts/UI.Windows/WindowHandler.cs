using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MIG.API;
using UObject = UnityEngine.Object;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class WindowHandler : IWindowHandler
    {
        private readonly ISceneLoadNotifier _sceneLoadNotifier;
        private readonly WindowHandlerSettings _settings;

        private readonly Dictionary<Type, IWindowController> _windowControllers = new();
        private readonly Dictionary<Type, IWindowFactory> _windowFactories = new();
        private readonly Dictionary<Type, IWindow> _windowsCache = new();

        private readonly Dictionary<int, IWindow> _viewWindows = new();
        private readonly Dictionary<int, Type> _viewWindowTypes = new();
        private readonly List<int> _windowsOrder = new();

        private WindowCanvas _cacheCanvas, _viewCanvas;
        private int _windowCounter;

        public WindowHandler(
            ISceneLoadNotifier sceneLoadNotifier,
            WindowHandlerSettings settings,
            IReadOnlyList<IWindowController> windowControllers,
            IReadOnlyList<IWindowFactory> windowFactories
        )
        {
            _sceneLoadNotifier = sceneLoadNotifier;
            _settings = settings;

            foreach (var controller in windowControllers)
            {
                _windowControllers[controller.GetWindowType()] = controller;
            }

            foreach (var factory in windowFactories)
            {
                _windowFactories[factory.GetWindowType()] = factory;
            }
        }

        public void Init()
        {
            _sceneLoadNotifier.BeforeSceneLoad += OnBeforeSceneLoad;
            _cacheCanvas = CreateCanvas(_settings.CacheCanvasName);
            _viewCanvas = CreateCanvas(_settings.ViewCanvasName);
        }

        public int OpenWindow<TWindow>() where TWindow : IWindow
        {
            var key = typeof(TWindow);
            if (!_windowsCache.TryGetValue(key, out var window))
            {
                window = _windowFactories[key].Create();
            }
            else
            {
                _windowsCache.Remove(key);
            }

            var windowId = ++_windowCounter;
            _viewWindows[windowId] = window;
            _viewWindowTypes[windowId] = key;

            _windowsOrder.Add(windowId);
            _windowControllers[key].SetWindow(window);

            PutWindowToViewCanvas(window);
            window.Open();

            return windowId;
        }

        public int OpenWindow<TWindow, TData>(TData data) where TWindow : IWindow<TData> where TData : IWindowData
        {
            var key = typeof(TWindow);
            if (!_windowsCache.TryGetValue(key, out var window))
            {
                window = _windowFactories[key].Create();
            }
            else
            {
                _windowsCache.Remove(key);
            }

            var windowId = ++_windowCounter;
            _viewWindows[windowId] = window;
            _viewWindowTypes[windowId] = key;

            _windowsOrder.Add(windowId);
            _windowControllers[key].SetWindow(window);

            ((TWindow)window).SetData(data);
            PutWindowToViewCanvas(window);
            window.Open();

            return windowId;
        }

        public void CloseWindow<TWindow>() where TWindow : IWindow
        {
            var windowId = _viewWindows.FirstOrDefault(pair => pair.Value is TWindow).Key;
            if (windowId == 0) return;
            CloseWindow(windowId);
        }

        public void CloseWindow(int windowId)
        {
            if (!_viewWindows.TryGetValue(windowId, out var window))
            {
                return;
            }

            window.Close();
            PutWindowToCacheCanvas(window);

            var windowType = _viewWindowTypes[windowId];
            _windowsCache[windowType] = window;
            _viewWindows.Remove(windowId);
            _viewWindowTypes.Remove(windowId);
            _windowControllers[windowType].ClearWindow();
            _windowsOrder.Remove(windowId);
        }

        public void CloseAllWindows()
        {
            foreach (var windowId in _viewWindows.Keys)
            {
                var window = _viewWindows[windowId];
                window.Close();
                PutWindowToCacheCanvas(window);
                var windowType = _viewWindowTypes[windowId];
                _windowsCache[windowType] = window;
                _windowControllers[windowType].ClearWindow();
            }

            _viewWindows.Clear();
            _viewWindowTypes.Clear();
            _windowsOrder.Clear();
        }

        private void PutWindowToCacheCanvas(IWindow window)
            => PutWindowToCanvas(window, _cacheCanvas);

        private void PutWindowToViewCanvas(IWindow window)
            => PutWindowToCanvas(window, _viewCanvas);

        private void PutWindowToCanvas(IWindow window, WindowCanvas canvas)
            => window.Attach(canvas.WindowsRoot);

        private WindowCanvas CreateCanvas(string canvasName)
        {
            var result = UObject.Instantiate(_settings.WindowCanvasPrefab);
            var resultGameObject = result.gameObject;
            resultGameObject.name = canvasName;
            UObject.DontDestroyOnLoad(resultGameObject);
            return result;
        }

        private void OnBeforeSceneLoad()
        {
            CloseAllWindows();
        }
    }
}