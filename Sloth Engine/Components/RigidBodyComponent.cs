using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Sloth_Engine.Components
{
    public class RigidBodyComponent : Component
    {
        private Matrix4 previousTransform = Matrix4.CreateTranslation(0, 0, 0);
        private Vector2 gravity = Vector2.Zero;

        public override ComponentType Type
        {
            get { return ComponentType.RigidBody; }
        }

        public Matrix4 PreviousTransform
        {
            get { return previousTransform; }
            set { previousTransform = value; }
        }

        public Vector2 Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
    }
}
