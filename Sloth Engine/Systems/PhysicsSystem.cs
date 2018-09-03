using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Objects;
using Sloth_Engine.Managers;
using Sloth_Engine.Components;
using OpenTK;

namespace Sloth_Engine.Systems
{
    public class PhysicsSystem : System, ISystem
    {
        public PhysicsSystem() : base()
        {

        }

        public string Name
        {
            get { return "Physics"; }
        }

        public override void onUpdate()
        {
            base.onUpdate();

            foreach (Entity e in EntityManager.Entities)
            {
                if (e.hasComponentWithType(ComponentType.RigidBody))
                {
                    bool noFall = false;

                    if (e.hasComponentWithType(ComponentType.Collision))
                    {
                        if (((CollisionComponent)e.Components[e.compRefFromType(ComponentType.Collision)]).IsCollided)
                        {
                            ((TransformComponent)e.Components[e.compRefFromType(ComponentType.Transform)]).Transform =
                                ((RigidBodyComponent)e.Components[e.compRefFromType(ComponentType.RigidBody)]).PreviousTransform;

                            noFall = true;
                        }
                    }

                    int transformRef = e.compRefFromType(ComponentType.Transform);
                    int rigidBodyRef = e.compRefFromType(ComponentType.RigidBody);

                    if (!noFall)
                    {
                        ((TransformComponent)e.Components[transformRef]).addToPosition(((RigidBodyComponent)e.Components[rigidBodyRef]).Gravity);
                    }


                    Matrix4 currentTransform = ((TransformComponent)e.Components[transformRef]).Transform;
                    ((RigidBodyComponent)e.Components[rigidBodyRef]).PreviousTransform = currentTransform;
                }
            }
        }
    }
}
