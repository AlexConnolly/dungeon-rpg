using Microsoft.Xna.Framework.Audio;

namespace LDG.Audio
{
    public class AudioClipConfig
    {
        public AudioClip Clip { get; set; }
        public Range Pitch { get; set; } = new Range(1, 1);

        public SoundEffectInstance ToSoundEffect()
        {
            Clip.Pitch = Pitch.GenerateRandom();

            return Clip.Effect;
        }
    }
}
