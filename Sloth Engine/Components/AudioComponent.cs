using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Managers;

namespace Sloth_Engine.Components
{
    public class AudioComponent : Component
    {
        private int audio;
        private bool triggered = false;

        public override ComponentType Type => ComponentType.Audio;

        public AudioComponent(String AudioName) : base()
        {
            audio = ResourceManager.LoadAudio(AudioName);
        }

        public int Audio
        {
            get { return audio; }
            set { audio = value; }
        }

        public bool Triggered
        {
            get { return triggered; }
            set { triggered = value; }
        }
    }
}
