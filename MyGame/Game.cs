using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Systems;
using Sloth_Engine.Managers;
using Sloth_Engine.Objects;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MyGame
{
    class Game : GameWindow
    {
        private List<Sloth_Engine.Systems.System> systems;
        RenderSystem rend;
        SceneManager sceneManager;

        public Game() : base
            (
            1152,
            640,
            GraphicsMode.Default,
            "My Game",
            GameWindowFlags.FixedWindow,
            DisplayDevice.Default,
            3,
            3,
            GraphicsContextFlags.ForwardCompatible
            )
        { }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            Matrix4 world = Matrix4.CreateOrthographic(Width, Height, 0, 1);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref world);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Black);

            systems = new List<Sloth_Engine.Systems.System>();

            systems.Add(new CollisionSystem());
            systems.Add(new PhysicsSystem());
            systems.Add(new AudioSystem());

            rend = new RenderSystem();
            sceneManager = new SceneManager();
            sceneManager.setScene(new SceneDefault(sceneManager));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnUpdateFrame(e);

            sceneManager.OnUpdate(e);

            foreach (Sloth_Engine.Systems.System s in systems)
            {
                s.onUpdate();
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.Key == Key.Escape)
            {
                Exit();
            }

            sceneManager.onKeyDown(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.MatrixMode(MatrixMode.Modelview);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            rend.onUpdate(e);
            sceneManager.OnRender(e);

            GL.Flush();
            SwapBuffers();
        }
    }
}
