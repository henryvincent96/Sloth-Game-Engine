using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Sloth_Engine.Scenes
{
    public interface IScene
    {
        void update(FrameEventArgs e);
        void render(FrameEventArgs e);
    }
}
