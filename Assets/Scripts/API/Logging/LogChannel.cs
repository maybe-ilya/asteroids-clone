using System;
using UnityEngine;

namespace MIG.API
{
    [Serializable]
    public struct LogChannel : IEquatable<LogChannel>
    {
        [SerializeField] [HideInInspector] private string _name;
        [SerializeField] [HideInInspector] private int _hash;

        public LogChannel(string name)
        {
            _name = name;
            _hash = _name.GetHashCode();
        }

        public override int GetHashCode()
            => _hash;


        public override bool Equals(object obj)
            => obj is LogChannel channel && Equals(channel);

        public bool Equals(LogChannel other)
            => _hash == other._hash;

        public override string ToString()
            => _name;
    }
}