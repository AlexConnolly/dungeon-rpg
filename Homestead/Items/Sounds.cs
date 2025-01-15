using LDG.Audio;

namespace Homestead.Items
{
    public static class Sounds
    {
        public static AudioClip Axe { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/AxeHit.wav");
        public static AudioClip ItemDrop { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/ItemDrop.wav");

        public static AudioClip ItemPickup { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/ItemPickup.wav");
    }
}
