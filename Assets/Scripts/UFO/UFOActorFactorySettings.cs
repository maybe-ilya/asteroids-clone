using System;
using MIG.API;
using UnityEngine;

namespace MIG.UFO
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(UFOActorFactorySettings))]
    public sealed class UFOActorFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private UFOActor _ufoActorPrefab;
        [SerializeField] [CheckObject] private Sprite[] _ufoSprites = Array.Empty<Sprite>();
        [SerializeField] private int _initialPoolSize;

        public UFOActor UfoActorPrefab => _ufoActorPrefab;
        public Sprite[] UfoSprites => _ufoSprites;
        public int InitialPoolSize => _initialPoolSize;
    }
}