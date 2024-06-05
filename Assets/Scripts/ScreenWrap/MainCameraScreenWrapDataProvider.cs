using System;
using UnityEngine;

namespace MIG.ScreenWrapping
{
    public sealed class MainCameraScreenWrapDataProvider : IScreenWrapDataProvider
    {
        public (float, float) GetScreenDimensions()
        {
            var camera = Camera.main;
            if (!camera)
            {
                throw new NullReferenceException($"No main camera");
            }

            var height = camera.orthographicSize * 2;
            var width = height * camera.aspect;
            return (height, width);
        }
    }
}