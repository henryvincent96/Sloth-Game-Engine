using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Objects;

namespace Sloth_Engine.Components
{
    public enum ComponentType
    {
        Transform,
        Texture,
        Collision,
        RigidBody,
        Audio,
        None
    }

    public class Component
    {
        private Guid handle;
        private Guid entityHandle;
        
        public virtual ComponentType Type
        {
            get { return ComponentType.None; }
        }

        /// <summary>
        /// Initialiser which generates new ID for component and take handle for object
        /// </summary>
        /// <param name="pEntityID">Object handle</param>
        public Component()
        {
            handle = Guid.NewGuid();
        }

        public Guid Handle
        {
            get
            {
                return handle;
            }
            set
            {
                handle = value;
            }
        }

        public Guid EntityHandle
        {
            get { return entityHandle; }
            set { entityHandle = value; }
        }

        public bool Equals(Component c)
        {
            if(c.Handle == handle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
