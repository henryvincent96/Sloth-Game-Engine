using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Objects;
using Sloth_Engine.Managers;

namespace Sloth_Engine.Components
{
    public class CollisionComponent : Component
    {
        private bool isCollided;

        public override ComponentType Type
        {
            get { return ComponentType.Collision; }
        }

        public CollisionComponent() : base()
        {

        }

        public bool IsCollided
        {
            get { return isCollided; }
            set { isCollided = value; }
        }
    }
}
