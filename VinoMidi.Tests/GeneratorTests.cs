using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note = Melanchall.DryWetMidi.Interaction.Note;

namespace VinoMidi.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void FindingCorrectPairsFromNotesList()
        {
            Generator g = new Generator("~/");
            List<Note> notes = new List<Note>() { 
                new Note(NoteName.A, 4, 100, 0),
                new Note(NoteName.CSharp, 4, 100, 0),
                new Note(NoteName.D, 4, 100, 100)
            };
            var pairs = g.ExtractPairsFromNotes(notes);
            Assert.AreEqual(1, pairs.Count);

            List<Note> notes2 = new List<Note>() {
                new Note(NoteName.A, 4, 100, 0),
                new Note(NoteName.CSharp, 4, 100, 0),
                new Note(NoteName.E, 5, 100, 100),
                new Note(NoteName.F, 5, 100, 200),
            };
            var pairs2 = g.ExtractPairsFromNotes(notes2);
            Assert.AreEqual(2, pairs2.Count);
        }
    }
}
