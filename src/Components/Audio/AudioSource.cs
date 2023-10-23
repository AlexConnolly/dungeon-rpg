using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Audio
{
    public class AudioSource : GameComponent
    {
        private readonly SoundEffect _sound;

        private SoundEffectInstance instance;

        public AudioSource(GameObject gameObject, SoundEffect sound) : base(gameObject)
        {
            this._sound = sound;
        }

        public bool IsPlaying()
        {
            return instance != null && instance.State == SoundState.Playing;
        }

        public void Loop()
        {
            if(!IsPlaying())
            {
                this.Start();
            }

            this.instance.IsLooped = true;
        }

        public void Start()
        {
            if(instance != null)
            {
                this.Stop();
            }

            instance = _sound.CreateInstance();
            instance.Play();
        }

        public void Stop()
        {
            instance.Stop();
            instance = null;
        }
    }
}
