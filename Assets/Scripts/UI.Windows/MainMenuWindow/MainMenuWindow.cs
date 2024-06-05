using System;
using MIG.API;
using UnityEngine;
using UnityEngine.UI;

namespace MIG.UI.Windows
{
    public sealed class MainMenuWindow : AbstractWindow, IMainMenuWindow
    {
        [SerializeField] [CheckObject] private Button _playButton;
        [SerializeField] [CheckObject] private Button _quitButton;

        public event Action OnPlayGameClick;
        public event Action OnQuitGameClick;

        protected override void BeforeOpen()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        protected override void BeforeClose()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
            => OnPlayGameClick?.Invoke();

        private void OnQuitButtonClicked()
            => OnQuitGameClick?.Invoke();
    }
}