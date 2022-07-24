
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace VinoMidi
{
    public class Generator
    {
        private readonly string _directory;

        public Generator(string dir)
        {
            _directory = dir;
        }

        public Chain Generate()
        {
            List<Pair> pairs = new List<Pair>();
            string[] files = Directory.GetFiles(_directory).Where(x => x.Contains(".mid")).ToArray();

            foreach (string file in files)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    MidiFile mf = MidiFile.Read(fs);
                    var notes = mf.GetNotes();

                    var newPairs = ExtractPairsFromNotes(notes);
                    pairs.AddRange(newPairs);
                }
            }

            Chain chain = new Chain(pairs);
            return chain;
        }

        public List<Pair> ExtractPairsFromNotes(ICollection<Note> notes)
        {
            List<Pair> pairs = new List<Pair>();
            List<MidiNode> midiNodes = new List<MidiNode>();
            Note lastNote = null;
            MidiNode newNode = new MidiNode();

            foreach (Note note in notes)
            {
                // Start or continue building the current node
                if (lastNote == null || lastNote.Time == note.Time)
                {
                    newNode.AddNote(note);
                }
                else {
                    midiNodes.Add(newNode);
                    newNode = new MidiNode(note);
                }
                lastNote = (Note)note.Clone();
            }
            // Add the last node to the list
            midiNodes.Add(newNode);

            for (int i = 0; i < midiNodes.Count-1; i++)
            {
                MidiNode nodeA = midiNodes[i];
                MidiNode nodeB = midiNodes[i + 1];
                Pair pair = new Pair(nodeA, nodeB);
                pairs.Add(pair);
            }

            return pairs;
        }
    }
}