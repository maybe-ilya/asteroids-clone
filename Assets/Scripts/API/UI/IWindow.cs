using UnityEngine;

namespace MIG.API
{
    public interface IWindow
    {
        void Open();
        void Close();
        void Attach(Transform target);
        void Release();
    }

    public interface IWindow<in TData> : IWindow where TData : IWindowData
    {
        void SetData(TData input);
    }

    public interface IWindowData
    {
    }
}