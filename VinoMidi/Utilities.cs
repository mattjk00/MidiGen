using MathNet.Numerics.LinearAlgebra;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
    public static class Utilities
    {
        public static bool NoteEquals(this Note a, Note b)
        {
            return a.Length == b.Length && a.NoteName == b.NoteName;
        }

        public static int RandomSelection(Vector<float> probabilities)
        {
            Random r = new Random();
            float x = (float)r.NextDouble();
            float sum = 0.0f;
            for (int i = 0; i < probabilities.Count; i++)
            {
                sum += probabilities[i];
                if (x < sum)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public enum KeySignature
    {
        C,
        CSharp,
        DFlat,
        D,
        DSharp,
        EFlat,
        E,
        ESharp,
        FFlat,
        F,
        FSharp,
        GFlat,
        G,
        GSharp,
        AFlat,
        A,
        ASharp,
        BFlat,
        B,
        BSharp,
        CFlat
    }
}
