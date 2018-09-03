using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Components;
using Sloth_Engine.Objects;
using OpenTK;

namespace Sloth_Engine.Components
{
    public class TransformComponent : Component
    {
        Matrix4 transform;
        Vector2 position;
        Vector2 scale;
        float rotation;

        public override ComponentType Type
        {
            get { return ComponentType.Transform; }
        }

        public TransformComponent(Matrix4 pTransform) : base()
        {
            transform = pTransform;
            position = transform.ExtractTranslation().Xy;
            scale = transform.ExtractScale().Xy;
            rotation = transform.ExtractRotation().Z;
        }

        public Matrix4 Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public Vector2 Position
        {
            get { return transform.ExtractTranslation().Xy; }
            set
            {
                position = value;
                calculateTransform();
            }
        }

        public void addToPosition(Vector2 positionToAdd)
        {
            position += positionToAdd;
            calculateTransform();
        }

        public Vector2 Scale
        {
            get { return transform.ExtractScale().Xy; }
            set
            {
                scale = value;
                calculateTransform();
            }
        }

        public void addToScale(Vector2 scaleToAdd)
        {
            scale += scaleToAdd;
            calculateTransform();
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                calculateTransform();
            }
        }

        public void addToRotation(float rotationToAdd)
        {
            rotation += rotationToAdd;
            calculateTransform();
        }

        private void calculateTransform()
        {
            transform = Matrix4.CreateScale(new Vector3(scale))
                * Matrix4.CreateRotationZ(-rotation * (float)(Math.PI / 180))
                * Matrix4.CreateTranslation(new Vector3(position.X, position.Y, 0));
        }
    }
}
