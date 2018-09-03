using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using Sloth_Engine.Scenes;
using Sloth_Engine.Objects;

namespace Sloth_Engine.Managers
{
    public class SceneManager
    {
        public Scene scene;

        public delegate void SceneDeligate(FrameEventArgs e);
        public SceneDeligate updater;
        public SceneDeligate renderer;

        public delegate void InputDeligate(KeyboardKeyEventArgs e);
        public InputDeligate keyDowner;

        public KeyboardState keyboardState, previousKeyboardState;

        public SceneManager()
        {
        }

        public void setScene(Scene newScene)
        {
            scene = newScene;
        }

        public void OnUpdate(FrameEventArgs e)
        {
            if(scene != null)
            updater(e);
        }

        public void onKeyDown(KeyboardKeyEventArgs e)
        {
            if(scene != null)
            {
                keyboardState = Keyboard.GetState();
                keyDowner(e);
                previousKeyboardState = keyboardState;
            }
        }

        public void OnRender(FrameEventArgs e)
        {
            if(scene != null)
            {
                renderer(e);
            }
        }
    }
}
