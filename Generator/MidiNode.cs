using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
    public class MidiNode : IEquatable<MidiNode>, ICloneable
    {
        private List<Note> _notes;

        public MidiNode(params Note[] notes)
        {
            _notes = notes.ToList();
        }

        public void AddNote(Note n)
        {
            _notes.Add(n);
        }

        public object Clone()
        {
            MidiNode clone = new MidiNode();
            foreach (Note note in _notes)
            {
                clone.AddNote((Note)note.Clone());
            }
            return clone;
        }

        public bool Equals(MidiNode? other)
        {
            if (other == null || other._notes.Count != _notes.Count) return false;

            for (int i = 0; i < _notes.Count; i++)
            {
                Note noteA = _notes[i];
                Note noteB = other._notes[i];

                if (noteA.NoteEquals(noteB) == false) return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            foreach (Note n in _notes)
            {
                hashCode.Add(n.NoteName);
                hashCode.Add(n.Octave);
                hashCode.Add(n.Length);
            }
            return hashCode.ToHashCode();
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            _notes.ForEach(n =>
            {
                sb.Append(n.NoteName);
                sb.Append(' ');
            });
            return $"{sb.ToString()}";
        }
    }
}
