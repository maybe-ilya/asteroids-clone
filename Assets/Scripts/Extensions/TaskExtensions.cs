using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MIG.Extensions
{
    public static class TaskExtensions
    {
        // https://forum.unity.com/threads/untity-synchronization-context-task-run-huge-performance-hit.740102/#post-8378859
        public static async void Forget(this Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}