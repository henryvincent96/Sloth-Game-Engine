using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Managers;
using Sloth_Engine.Objects;
using Sloth_Engine.Components;
using OpenTK;

namespace Sloth_Engine.Systems
{
    public class CollisionSystem : System, ISystem
    {
        private List<Guid> entityHandles = new List<Guid>();
        private float boxSize = 50;

        public CollisionSystem() : base()
        {

        }

        public string Name
        {
            get { return "Collision"; }
        }

        public override void onUpdate()
        {
            entityHandles = new List<Guid>();

            foreach (Entity e in EntityManager.Entities)
            {
                if (e.hasComponentWithType(ComponentType.Collision))
                {
                    entityHandles.Add(e.Handle);
                }
            }

            foreach (Guid g in entityHandles)
            {
                checkForCollisions(g);
            }
        }

        private void checkForCollisions(Guid handle)
        {

            Matrix4 thisTransform = Matrix4.Identity;
            foreach (Entity ent in EntityManager.Entities)
            {
                if (ent.Handle == handle)
                {
                    thisTransform = ((TransformComponent)ent.Components
                        [ent.compRefFromType(ComponentType.Transform)]).Transform;
                    break;
                }
            }

            foreach (Entity ent in EntityManager.Entities)
            {
                if (ent.Handle != handle && ent.hasComponentWithType(ComponentType.Collision))
                {
                    Matrix4 transformInQuestion = ((TransformComponent)ent.Components
                        [ent.compRefFromType(ComponentType.Transform)]).Transform;

                    Vector2 thisPosition = thisTransform.ExtractTranslation().Xy;
                    Vector2 positionInQuestion = transformInQuestion.ExtractTranslation().Xy;

                    Vector2 scaleInQuestion = transformInQuestion.ExtractScale().Xy;

                    if (boxesCollide(thisTransform, transformInQuestion))
                    {
                        ((CollisionComponent)EntityManager.Entities[EntityManager.entityRefFromID(handle)].Components
                            [EntityManager.Entities[EntityManager.entityRefFromID(handle)].
                            compRefFromType(ComponentType.Collision)]).IsCollided = true;

                        ((CollisionComponent)ent.Components[ent.compRefFromType(ComponentType.Collision)]).IsCollided = true;
                        break;
                    }
                    else
                    {
                        ((CollisionComponent)EntityManager.Entities[EntityManager.entityRefFromID(handle)].Components
                            [EntityManager.Entities[EntityManager.entityRefFromID(handle)].
                            compRefFromType(ComponentType.Collision)]).IsCollided = false;
                    }
                }
            }
        }

        private bool boxesCollide(Matrix4 boxA, Matrix4 boxB)
        {
            Vector2 boxAPos = boxA.ExtractTranslation().Xy;
            float boxAWidth = boxSize * boxA.ExtractScale().X;
            float boxAHeight = boxSize * boxA.ExtractScale().Y;

            List<Vector2> corners = new List<Vector2>();

            corners.Add(boxAPos + new Vector2(-boxAWidth, -boxAHeight));
            corners.Add(boxAPos + new Vector2(-boxAWidth, boxAHeight));
            corners.Add(boxAPos + new Vector2(boxAWidth, -boxAHeight));
            corners.Add(boxAPos + new Vector2(boxAWidth, boxAHeight));

            foreach (Vector2 c in corners)
            {
                if (cornerCollides(c, ref boxB)) { return true; }
            }

            return false;
        }

        private bool cornerCollides(Vector2 corner, ref Matrix4 boxB)
        {
            Vector2 tl = Vector4.Transform(new Vector4(-boxSize, boxSize, 0, 1), boxB).Xy;
            Vector2 bl = Vector4.Transform(new Vector4(-boxSize, -boxSize, 0, 1), boxB).Xy;
            Vector2 tr = Vector4.Transform(new Vector4(boxSize, boxSize, 0, 1), boxB).Xy;
            Vector2 br = Vector4.Transform(new Vector4(boxSize, -boxSize, 0, 1), boxB).Xy;

            return (checkWallCollisionY(tl, bl, ref boxB, corner)) &&
             (checkWallCollisionY(tr, br, ref boxB, corner)) &&
             (checkWallCollisionX(tr, tl, ref boxB, corner)) &&
             (checkWallCollisionX(br, bl, ref boxB, corner));
        }

        private bool checkWallCollisionY(Vector2 coord1, Vector2 coord2, ref Matrix4 boxB, Vector2 point)
        {
            Vector2 left = coord1 - coord2;
            Vector2 lNormal = left.Normalized();
            Vector2 pointL2P = point - coord2;

            Vector2 positionOnLeft = Vector2.Dot(pointL2P, lNormal) * lNormal;
            Vector2 distLeftToPoint = coord2 + positionOnLeft - point;

            return (distLeftToPoint.Length >= 0) && (positionOnLeft.Y > 0 && positionOnLeft.Y < left.Length);
        }

        private bool checkWallCollisionX(Vector2 coord1, Vector2 coord2, ref Matrix4 boxB, Vector2 point)
        {
            Vector2 left = coord1 - coord2;
            Vector2 lNormal = left.Normalized();
            Vector2 pointL2P = point - coord2;

            Vector2 positionOnLeft = Vector2.Dot(pointL2P, lNormal) * lNormal;
            Vector2 distLeftToPoint = coord2 + positionOnLeft - point;

            return (distLeftToPoint.Length >= 0) && (positionOnLeft.X > 0 && positionOnLeft.X < left.Length);
        }
    }
}
