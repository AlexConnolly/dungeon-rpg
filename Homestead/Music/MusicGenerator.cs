using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Music
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;

    public class MusicGenerator
    {
        private int bpm;
        private int bars;
        private int sampleRate = 44100;
        private int[] chordProgression;

        public MusicGenerator(int bpm = 100, int bars = 8)
        {
            this.bpm = bpm;
            this.bars = bars;
            GenerateChordProgression();
        }

        private void GenerateChordProgression()
        {
            // Basic four-chord progression (e.g., I-V-vi-IV)
            chordProgression = new int[] { 0, 4, 5, 3 }; // Assuming C major: C, G, Am, F
        }

        public SoundEffectInstance GenerateMusic()
        {
            int beatsPerBar = 4;
            int totalBeats = bars * beatsPerBar;
            int samplesPerBeat = sampleRate * 60 / bpm;
            int totalSamples = samplesPerBeat * totalBeats;

            float[] samples = new float[totalSamples];

            for (int bar = 0; bar < bars; bar++)
            {
                int chordIndex = chordProgression[bar % chordProgression.Length];
                float[] chordSamples = GenerateChordSamples(chordIndex, samplesPerBeat * beatsPerBar);

                for (int i = 0; i < chordSamples.Length; i++)
                {
                    samples[bar * samplesPerBeat * beatsPerBar + i] += chordSamples[i];
                }
            }

            // Add melody on top
            float[] melodySamples = GenerateMelody(totalSamples);
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] += melodySamples[i];
            }

            // Add kick drum on each beat
            float[] kickSamples = GenerateKick(totalSamples, samplesPerBeat);
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] += kickSamples[i];
            }

            // Normalize combined samples
            float maxAmplitude = 0f;
            foreach (float sample in samples)
            {
                maxAmplitude = Math.Max(maxAmplitude, Math.Abs(sample));
            }
            if (maxAmplitude > 0)
            {
                for (int i = 0; i < samples.Length; i++)
                {
                    samples[i] /= maxAmplitude;
                }
            }

            byte[] audioData = new byte[totalSamples * sizeof(short)];
            for (int i = 0; i < samples.Length; i++)
            {
                short sampleValue = (short)(samples[i] * short.MaxValue);
                audioData[i * 2] = (byte)(sampleValue & 0xFF);
                audioData[i * 2 + 1] = (byte)((sampleValue >> 8) & 0xFF);
            }

            SoundEffect soundEffect = new SoundEffect(audioData, sampleRate, AudioChannels.Mono);
            return soundEffect.CreateInstance();
        }

        private float[] GenerateChordSamples(int chordIndex, int length)
        {
            // Basic frequencies for C major (C, E, G)
            float[][] chords = new float[][]
            {
            new float[] { 261.63f, 329.63f, 392.00f }, // C major
            new float[] { 196.00f, 246.94f, 329.63f }, // G major
            new float[] { 220.00f, 261.63f, 329.63f }, // A minor
            new float[] { 174.61f, 220.00f, 261.63f }  // F major
            };

            float[] frequencies = chords[chordIndex % chords.Length];
            float[] samples = new float[length];

            for (int i = 0; i < length; i++)
            {
                float t = (float)i / sampleRate;
                foreach (float freq in frequencies)
                {
                    samples[i] += (float)Math.Sin(2 * Math.PI * freq * t);
                }

                // Normalize to avoid clipping
                samples[i] /= frequencies.Length;
            }

            return samples;
        }

        private float[] GenerateMelody(int length)
        {
            // Melody relative to the current chord
            float[][] chordTones = new float[][]
            {
            new float[] { 130.81f, 164.81f, 196.00f }, // C major (lower octave)
            new float[] { 98.00f, 123.47f, 164.81f },  // G major (lower octave)
            new float[] { 110.00f, 130.81f, 164.81f }, // A minor (lower octave)
            new float[] { 87.31f, 110.00f, 130.81f }   // F major (lower octave)
            };

            int beatsPerBar = 4;
            int samplesPerBeat = sampleRate * 60 / bpm;
            float[] samples = new float[length];

            for (int bar = 0; bar < bars; bar++)
            {
                int chordIndex = chordProgression[bar % chordProgression.Length];
                float[] currentChordTones = chordTones[chordIndex % chordTones.Length];

                for (int beat = 0; beat < beatsPerBar; beat++)
                {
                    if (beat != 1) continue; // Melody starts at beat 2

                    int startSample = (bar * beatsPerBar + beat) * samplesPerBeat;

                    // Play note 1, note 2, note 1 in extended succession
                    for (int note = 0; note < 3; note++)
                    {
                        int noteStart = startSample + note * samplesPerBeat / 2;
                        int noteEnd = noteStart + samplesPerBeat / 2;
                        float melodyNote = (note % 2 == 0) ? currentChordTones[0] : currentChordTones[1];

                        for (int i = noteStart; i < noteEnd && i < length; i++)
                        {
                            float t = (float)(i - noteStart) / sampleRate;
                            samples[i] += (float)Math.Sin(2 * Math.PI * melodyNote * t) * 0.3f; // Softer melody
                        }
                    }

                    // Pause
                    int pauseStart = startSample + 3 * samplesPerBeat / 2;
                    int pauseEnd = startSample + 2 * samplesPerBeat;
                    for (int i = pauseStart; i < pauseEnd && i < length; i++)
                    {
                        samples[i] = 0;
                    }

                    // Play note 1 again
                    int finalNoteStart = startSample + 2 * samplesPerBeat;
                    int finalNoteEnd = finalNoteStart + samplesPerBeat / 2;
                    float finalNote = currentChordTones[0];

                    for (int i = finalNoteStart; i < finalNoteEnd && i < length; i++)
                    {
                        float t = (float)(i - finalNoteStart) / sampleRate;
                        samples[i] += (float)Math.Sin(2 * Math.PI * finalNote * t) * 0.3f;
                    }
                }
            }

            return samples;
        }

        private float[] GenerateKick(int totalSamples, int samplesPerBeat)
        {
            float[] samples = new float[totalSamples];
            int beats = totalSamples / samplesPerBeat;

            for (int beat = 0; beat < beats; beat++)
            {
                int startSample = beat * samplesPerBeat;
                int kickLength = samplesPerBeat / 4;

                for (int i = 0; i < kickLength && startSample + i < totalSamples; i++)
                {
                    float t = (float)i / sampleRate;
                    float envelope = 1.0f - (float)i / kickLength; // Simple decay envelope
                    samples[startSample + i] += (float)Math.Sin(2 * Math.PI * 60.0f * t) * envelope * 0.5f; // Kick frequency ~60Hz
                }
            }

            return samples;
        }

        public void SetBPM(int bpm)
        {
            this.bpm = bpm;
        }

        public void SetBars(int bars)
        {
            this.bars = bars;
        }
    }

}
