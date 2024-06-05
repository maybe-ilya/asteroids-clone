using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    public sealed class WindowCanvas : MonoBehaviour
    {
        [SerializeField] [CheckObject] private Transform _windowsRoot;
        
        public Transform WindowsRoot => _windowsRoot;
    }
}