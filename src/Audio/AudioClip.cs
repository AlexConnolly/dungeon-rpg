using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Audio
{
    public class AudioClip
    {
        public static AudioClip FromFile(string resource)
        {
            using (FileStream fileStream = new FileStream(resource, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                SoundEffect soundEffect = SoundEffect.FromStream(fileStream);
                SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

                return new AudioClip()
                {
                    Effect = soundEffectInstance
                };
            }
        }

        public static AudioClip FromSoundEffect(SoundEffectInstance instance)
        {
            return new AudioClip()
            {
                Effect = instance,
                Pitch = 1
            };
        }

        public SoundEffectInstance Effect { get; private set; }

        public float Pitch
        {
            get
            {
                return Effect.Pitch;
            }

            set
            {
                Effect.Pitch = value;
            }
        }

        public AudioClip SetPitch(float pitch)
        {
            Pitch = pitch;

            return this;
        }

        public void Play()
        {
            Effect.Play();
        }
    }
}
