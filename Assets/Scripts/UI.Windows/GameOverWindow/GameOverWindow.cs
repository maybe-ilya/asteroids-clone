using System;
using MIG.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MIG.UI.Windows
{
    public sealed class GameOverWindow : AbstractWindow, IGameOverWindow
    {
        [SerializeField] [CheckObject] private Button _retryButton;
        [SerializeField] [CheckObject] private Button _exitButton;
        [SerializeField] [CheckObject] private TMP_Text _scoreLabel;

        public event Action OnRetryClicked;
        public event Action OnExitClicked;

        public void SetData(GameOverWindowData input)
        {
            _scoreLabel.text = input.Score.ToString();
        }

        protected override void BeforeOpen()
        {
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        protected override void BeforeClose()
        {
            _retryButton.onClick.RemoveListener(OnRetryButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnRetryButtonClicked()
            => OnRetryClicked?.Invoke();

        private void OnExitButtonClicked()
            => OnExitClicked?.Invoke();
    }
}