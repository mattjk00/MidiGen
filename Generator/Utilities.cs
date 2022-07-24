using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
    internal static class Utilities
    {
        public static bool NoteEquals(this Note a, Note b)
        {
            return a.Length == b.Length && a.NoteName == b.NoteName;
        }
    }
}
