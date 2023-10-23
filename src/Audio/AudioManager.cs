using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Audio
{
    public static class AudioManager
    {
        private static Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect>();

        public static void Load(ContentManager Content)
        {
            _sounds.Add("character_footsteps", Content.Load<SoundEffect>("Audio/FX/Characters/stepdirt_1"));
        }

        public static SoundEffect GetSound(string name)
        {
            if (_sounds.ContainsKey(name))
                return _sounds[name];

            return null;
        }
    }
}
