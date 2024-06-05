using UnityEngine;

namespace MIG.Editor
{
    [GameConfig("Library/CheckObjectPropertyDrawerSettings.asset",
        "Preferences/CheckObject Property Drawer Settings",
        true)]
    public sealed class CheckObjectPropertyDrawerSettings : GameConfig
    {
        [SerializeField] private Color _validObjectColor = Color.green, _invalidObjectColor = Color.red;

        public Color ValidObjectColor => _validObjectColor;

        public Color InvalidObjectColor => _invalidObjectColor;
    }
}