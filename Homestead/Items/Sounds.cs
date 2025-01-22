using LDG.Audio;

namespace Homestead.Items
{
    public static class Sounds
    {
        public static AudioClip Axe { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/AxeHit.wav");
        public static AudioClip ItemDrop { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/ItemDrop.wav");

        public static AudioClip ItemPickup { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/ItemPickup.wav");
        public static AudioClip OhYeah { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/OhYeah.wav");

        public static AudioClip ItemEquip { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/ItemEquip.wav");
        public static AudioClip Walking { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/Walking.wav");
        public static AudioClip TreeShake { get; } = AudioManager.GetOrLoadClip("Media/Audio/Effects/TreeShake.wav");
    }
}
