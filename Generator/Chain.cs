using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
    public class Chain
    {
        private Dictionary<Pair, int> _transitions;
        private Dictionary<MidiNode, int> _nodeSet;

        private float[,] _tmatrix;

        public Chain(List<Pair> pairs)
        {
            _tmatrix = new float[0, 0];
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
            _tmatrix = new float[count,count];

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

        private void RegisterMidiNode(MidiNode mn)
        {
            if (_nodeSet.ContainsKey(mn) == false)
            {
                _nodeSet[mn] = _nodeSet.Keys.Count;
            }
        }
    }
}
