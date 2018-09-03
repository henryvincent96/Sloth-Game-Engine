using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Managers;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Sloth_Engine.Objects
{
    public class Scene
    {
        SceneManager sceneManager;

        public Scene(SceneManager pSceneManager)
        {
            sceneManager = pSceneManager;
            EntityManager.nukeEntities();
            GL.ClearColor(Color4.Black);
        }
    }
}
