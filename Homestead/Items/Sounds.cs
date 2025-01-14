using LDG.Audio;

namespace Homestead.Items
{
    public static class Sounds
    {
        public static AudioClip Axe { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/AxeHit.wav");
    }
}
