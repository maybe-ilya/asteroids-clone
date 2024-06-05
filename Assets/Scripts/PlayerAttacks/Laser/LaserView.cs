using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    public sealed class LaserView : MonoBehaviour
    {
        [SerializeField] [CheckObject] private Animation _animation;

        public void SetLifeTime(float lifeTime)
        {
            Destroy(gameObject, lifeTime);
        }

        public void Show()
        {
            _animation.Play(PlayMode.StopAll);
        }
    }
}