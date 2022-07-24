using MathNet.Numerics.LinearAlgebra.Single;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note = Melanchall.DryWetMidi.Interaction.Note;

namespace VinoMidi
{
    public class Chain
    {
        private Dictionary<Pair, int> _transitions;
        private Dictionary<MidiNode, int> _nodeSet;

        //private float[,] _tmatrix;
        private SparseMatrix _tmatrix;

        public Chain(List<Pair> pairs)
        {
            _tmatrix = SparseMatrix.Create(0, 0, 0);//new float[0, 0];
            _nodeSet = new Dictionary<MidiNode, int>();
            _transitions = new Dictionary<Pair, int>();
            ParseTransitions(pairs);
        }

        private void ParseTransitions(List<Pair> pairs)
        {
            int total = pairs.Count;
            Dictionary<MidiNode, int> totalTransitionsForNode = new Dictionary<MidiNode, int>();

            foreach (Pair pair in pairs)
            {
                if (_transitions.ContainsKey(pair))
                {
                    _transitions[pair] = _transitions[pair] + 1;
                }
                else
                {
                    _transitions[pair] = 1;
                    RegisterMidiNode(pair.A);
                    RegisterMidiNode(pair.B);
                }

                // Keep track of how many transitions they are from this origin node A
                if (totalTransitionsForNode.ContainsKey(pair.A))
                {
                    totalTransitionsForNode[pair.A] = totalTransitionsForNode[pair.A] + 1;
                }
                else
                {
                    totalTransitionsForNode[pair.A] = 1;
                }
            }

            int count = _transitions.Keys.Count;
            _tmatrix = SparseMatrix.Create(count, count, 0.0f);//new float[count,count];

            foreach (Pair p in _transitions.Keys)
            {
                MidiNode originNode = p.A;
                int transToBCount = _transitions[p];
                int totalTrans = totalTransitionsForNode[originNode];
                float percentage = (float)transToBCount / (float)totalTrans;

                int aindex = _nodeSet[p.A];
                int bindex = _nodeSet[p.B];

                _tmatrix[aindex, bindex] = percentage;
            }
        }

        /// <summary>
        /// Creates a midi pattern.
        /// </summary>
        /// <param name="t">The number of transitions to take -- The length of the piece.</param>
        public Pattern CreateMidi(int t)
        {
            PatternBuilder builder = new PatternBuilder();
            Random rand = new Random();
            int N = _nodeSet.Count;
            int state = rand.Next(N);
            int nextState = -1;

            for (int i = 0; i < t; i++)
            {
                var transitionDist = _tmatrix.Row(state);
                while (nextState == -1) nextState = Utilities.RandomSelection(transitionDist);

                state = nextState;
                var midiNode = _nodeSet.FirstOrDefault(x => x.Value == state).Key;
                for (int j = 0; j < midiNode.Notes.Count; j++)
                {
                    if (j > 0)
                    {
                        builder.MoveToPreviousTime();
                    }
                    var n = midiNode.Notes[j];
                    builder.SetNoteLength(new MidiTimeSpan(n.Length));
                    builder.SetOctave(Octave.Get(n.Octave));
                    builder.Note(n.NoteName);
                }

                Console.WriteLine(midiNode);
                nextState = -1;
            }

            return builder.Build();
        }

        private void RegisterMidiNode(MidiNode mn)
        {
            if (_nodeSet.ContainsKey(mn) == false)
            {
                _nodeSet[mn] = _nodeSet.Keys.Count;
            }
        }

        public void PrintInfo()
        {
            foreach (MidiNode node in _nodeSet.Keys)
            {
                Console.WriteLine($"{_nodeSet[node]}: {node}");
            }
            Console.WriteLine(_tmatrix);
        }

        public SparseMatrix TransitionMatrix { get { return _tmatrix; } }
    }
}
