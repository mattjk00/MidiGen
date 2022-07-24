using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using VinoMidi;

Generator generator = new Generator("./");
var chain = generator.Generate();
chain.PrintInfo();
var midi = chain.CreateMidi(100);
MidiFile midiFile = midi.ToFile(TempoMap.Default);
FileStream fs = new FileStream("out/out.mid", FileMode.OpenOrCreate, FileAccess.ReadWrite);
midiFile.Write(fs);
fs.Close();

/*FileStream fs = new FileStream("test.mid", FileMode.Open, FileAccess.Read);
MidiFile mFile = MidiFile.Read(fs);
var notes = mFile.GetNotes();
foreach (var note in notes)
{
    Console.WriteLine("{0}\t{1}-{2}", note, note.Time, note.EndTime);
}*/

/*var pattern = new PatternBuilder()

    // Insert a pause of 5 seconds
    //.StepForward(new MetricTimeSpan(0, 0, 1))

    // Insert an eighth C# note of the 4th octave
    .Note(Octave.Get(4).C, MusicalTimeSpan.Whole)

    // Set default note length to triplet eighth and default octave to 5
    .SetNoteLength(MusicalTimeSpan.Eighth)
    .SetOctave(Octave.Get(5))
    .MoveToPreviousTime()

    // Now we can add triplet eighth notes of the 5th octave in a simple way
    .Note(NoteName.A)

    .Note(NoteName.B)
    .Note(NoteName.A)
    .Note(NoteName.B)
    .Note(NoteName.A)
    .SetNoteLength(MusicalTimeSpan.Whole)
    .Note(NoteName.G)
    .Build();

MidiFile midiFile = pattern.ToFile(TempoMap.Default);
FileStream fs = new FileStream("test.mid", FileMode.Open, FileAccess.ReadWrite);
midiFile.Write(fs);
fs.Close();
*/
Console.WriteLine("Done!");