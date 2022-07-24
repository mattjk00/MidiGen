using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Note = Melanchall.DryWetMidi.Interaction.Note;

namespace VinoMidi.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeEqualityComparisons()
        {
            MidiNode node1 = new MidiNode(new Note(NoteName.A, 4, 48, 0));
            MidiNode node2 = new MidiNode(new Note(NoteName.A, 4, 48, 0));
            Assert.IsTrue(node1.Equals(node2));

            MidiNode node3 = new MidiNode(new Note(NoteName.C, 4, 48, 0));
            MidiNode node4 = new MidiNode(new Note(NoteName.A, 4, 48, 0));
            Assert.IsTrue(!node3.Equals(node4));

            MidiNode node5 = new MidiNode(new Note(NoteName.C, 4, 48, 0), new Note(NoteName.D, 3, 48, 0));
            MidiNode node6 = new MidiNode(new Note(NoteName.A, 4, 48, 0));
            Assert.IsTrue(!node5.Equals(node6));
        }

        [TestMethod]
        public void PairEqualityComparisons()
        {
            MidiNode node1 = new MidiNode(new Note(NoteName.A, 4, 48, 0));
            MidiNode node2 = new MidiNode(new Note(NoteName.C, 4, 48, 0));

            Pair pair1 = new Pair(node1, node2);
            Pair pair2 = new Pair(node1, node2);

            Assert.IsTrue(pair1.Equals(pair2));
            Assert.AreEqual(pair1.GetHashCode(), pair2.GetHashCode());
        }
    }
}