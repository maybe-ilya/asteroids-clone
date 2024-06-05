using System;
using UnityEngine;

namespace MIG.Utils
{
    public sealed class GizmosMatrixScope : IDisposable
    {
        private readonly Matrix4x4 _originalMatrix;

        public GizmosMatrixScope(Matrix4x4 newMatrix)
        {
            _originalMatrix = Gizmos.matrix;
            Gizmos.matrix = newMatrix;
        }
        
        public void Dispose()
        {
            Gizmos.matrix = _originalMatrix;
        }
    }
}
