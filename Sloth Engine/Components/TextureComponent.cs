using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Objects;
using Sloth_Engine.Managers;

namespace Sloth_Engine.Components
{
    public class TextureComponent : Component
    {
        int texture;

        public override ComponentType Type => ComponentType.Texture;

        public TextureComponent(String TextureName) : base()
        {
            texture = ResourceManager.LoadTexture(TextureName);
        }

        public int Texture
        {
            get { return texture; }
        }
    }
}
