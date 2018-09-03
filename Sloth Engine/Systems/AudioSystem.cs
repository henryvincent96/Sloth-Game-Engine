using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Components;
using Sloth_Engine.Managers;
using Sloth_Engine.Objects;
using Sloth_Engine.Systems;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace Sloth_Engine.Systems
{
    public class AudioSystem : System, ISystem
    {
        private int source;
        private Vector3 position = Vector3.Zero;
        private Vector3 direction = new Vector3(0, 0, -1);
        private Vector3 orientation = Vector3.UnitY;
        private AudioContext audioContext = new AudioContext();

        public AudioSystem() : base()
        {
            source = AL.GenSource();
            AL.Source(source, ALSource3f.Position, ref position);
            AL.Listener(ALListener3f.Position, ref position);
            AL.Listener(ALListenerfv.Orientation, ref direction, ref orientation);
            AL.Listener(ALListenerf.Gain, 1);
            AL.Source(source, ALSourceb.Looping, false);
        }

        public string Name => "Audio";

        public override void onUpdate()
        {
            base.onUpdate();

            foreach(Entity ent in EntityManager.Entities)
            {
                if (ent.hasComponentWithType(ComponentType.Audio))
                {
                    if (((AudioComponent)ent.Components[ent.compRefFromType(ComponentType.Audio)]).Triggered)
                    {
                        if(AL.GetSourceState(source) != ALSourceState.Playing)
                        {
                            int buffer = ((AudioComponent)ent.Components[ent.compRefFromType(ComponentType.Audio)]).Audio;

                            AL.Source(source, ALSourcei.Buffer, buffer);
                            AL.SourcePlay(source);

                            ((AudioComponent)ent.Components[ent.compRefFromType(ComponentType.Audio)]).Triggered = false;
                        }
                    }
                }
            }
        }
    }
}
