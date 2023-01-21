using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi
{
	public class MidiTrack
	{
		private MidiFile _midiFile;
		public KeySignature KeySignature { get; set; }
		public MidiFile MidiFile { get => _midiFile; protected set => _midiFile = value; }

		public MidiTrack(MidiFile mf, KeySignature ks)
		{
			_midiFile = mf;
			KeySignature = ks;
		}

	}
}
