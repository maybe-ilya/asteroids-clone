using System;

namespace MIG.API
{
    public struct LeaderboardEntry :
        IEquatable<LeaderboardEntry>,
        IComparable<LeaderboardEntry>
    {
        public readonly string Name;
        public readonly int Score;

        public LeaderboardEntry(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override string ToString()
            => $"{Name}: {Score}";

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + Score.GetHashCode();
                hash = hash * 13 + StringComparer.InvariantCultureIgnoreCase.GetHashCode(Name);
                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is LeaderboardEntry other && Equals(other);

        public bool Equals(LeaderboardEntry other)
            => Score == other.Score &&
               Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);

        public int CompareTo(LeaderboardEntry other)
        {
            var scoreComp = Score.CompareTo(other.Score);
            return scoreComp != 0 ? scoreComp : StringComparer.InvariantCultureIgnoreCase.Compare(Name, other.Name);
        }

        public static implicit operator LeaderboardEntry((string name, int score) data)
            => new(data.name, data.score);
    }
}