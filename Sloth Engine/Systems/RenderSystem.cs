using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Sloth_Engine.Managers;
using Sloth_Engine.Objects;
using Sloth_Engine.Components;
using OpenTK;

namespace Sloth_Engine.Systems
{
    public class RenderSystem : System, ISystem
    {
        private int vertexHandle, vertexBufferObject;

        public RenderSystem() : base()
        {
            float size = 50f;

            float[] vertices = new float[]
            {
                -size,size,   0,0,
                size,size,    1,0,
                size,-size,   1,1,
                -size,-size,  0,1
            };

            GL.GenVertexArrays(1, out vertexHandle);
            GL.BindVertexArray(vertexHandle);
            GL.GenBuffers(1, out vertexBufferObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)),
                vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 4 * sizeof(float), 2 * sizeof(float));
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindVertexArray(0);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(0, BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public string Name
        {
            get { return "Render"; }
        }

        public override void onUpdate()
        {
            base.onUpdate();
            onUpdate(new FrameEventArgs());
        }

        public void onUpdate(FrameEventArgs e)
        {
            foreach (Entity ent in EntityManager.Entities)
            {
                Matrix4 transform = Matrix4.Identity;
                int texture = 0;

                if(ent.hasComponentWithType(ComponentType.Transform) &&
                    ent.hasComponentWithType(ComponentType.Texture))
                {
                    transform = ((TransformComponent)ent.Components
                        [ent.compRefFromType(ComponentType.Transform)]).Transform;
                    texture = ((TextureComponent)ent.Components
                        [ent.compRefFromType(ComponentType.Texture)]).Texture;
                }

                if (transform != Matrix4.Identity && texture != 0)
                {
                    Draw(e, transform, texture);
                }
            }
        }

        private void Draw(FrameEventArgs e, Matrix4 transform, int texture)
        {
            GL.LoadMatrix(ref transform);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.BindVertexArray(vertexHandle);
            GL.DrawArrays(PrimitiveType.Quads, 0, 8);

            GL.BindVertexArray(0);
        }
    }
}
