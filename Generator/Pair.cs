using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
    public class Pair : IEquatable<Pair>
    {
        private MidiNode _a;
        private MidiNode _b;
        private string _id;

        public MidiNode A { get { return _a; } }
        public MidiNode B { get { return _b; } }
        public string ID { get { return _id; } }

        public Pair(MidiNode a, MidiNode b) 
        {
            _a = a;
            _b = b;
            _id = Guid.NewGuid().ToString();
        }

        public bool Equals(Pair? other)
        {
            if (other == null) return false;

            return _a.Equals(other._a) && _b.Equals(other._b);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(_a.GetHashCode());
            hashCode.Add(_b.GetHashCode());
            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            return $"[{GetHashCode()}] {_a.ToString()}, {_b.ToString()}";
        }
    }
}
