using System;
using UnityEngine;

namespace MIG.Utils
{
    public sealed class GizmosColorScope : IDisposable
    {
        private readonly Color _originalColor;

        public GizmosColorScope(Color newColor)
        {
            _originalColor = Gizmos.color;
            Gizmos.color = newColor;
        }

        public void Dispose()
        {
            Gizmos.color = _originalColor;
        }
    }
}
