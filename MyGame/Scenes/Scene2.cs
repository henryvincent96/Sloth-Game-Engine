using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Scenes;
using Sloth_Engine.Objects;
using Sloth_Engine.Managers;
using Sloth_Engine.Components;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace MyGame
{
    class Scene2 : Scene, IScene
    {
        SceneManager sceneManager;
        Guid boxHandle, box2Handle, box3Handle;
        Vector2 movement;

        public Scene2(SceneManager inSceneManager) : base(inSceneManager)
        {
            sceneManager = inSceneManager;

            sceneManager.updater = update;
            sceneManager.keyDowner = onKeyDown;
            sceneManager.renderer = render;

            GL.ClearColor(Color4.Green);

            TransformComponent transform;
            TextureComponent texture;
            CollisionComponent collision;
            RigidBodyComponent rigidBody;

            boxHandle = EntityManager.createNewBlankEntity();

            transform = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(-450, -250),
                Scale = new Vector2(1, 1)
            };
            texture = new TextureComponent("box.bmp");
            collision = new CollisionComponent();
            rigidBody = new RigidBodyComponent();

            int boxRef = EntityManager.entityRefFromID(boxHandle);

            EntityManager.Entities[boxRef].addComponent(transform);
            EntityManager.Entities[boxRef].addComponent(texture);
            EntityManager.Entities[boxRef].addComponent(rigidBody);
            EntityManager.Entities[boxRef].addComponent(collision);

            box2Handle = EntityManager.createNewBlankEntity();

            transform = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(-200, 0),
                Scale = new Vector2(2, 2)
            };
            texture = new TextureComponent("box2.bmp");
            collision = new CollisionComponent();
            rigidBody = new RigidBodyComponent();

            int box2Ref = EntityManager.entityRefFromID(box2Handle);

            EntityManager.Entities[box2Ref].addComponent(transform);
            EntityManager.Entities[box2Ref].addComponent(texture);
            EntityManager.Entities[box2Ref].addComponent(rigidBody);
            EntityManager.Entities[box2Ref].addComponent(collision);

            box3Handle = EntityManager.createNewBlankEntity();

            transform = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(200, 0),
                Scale = new Vector2(2, 2),
                Rotation = 45
            };
            texture = new TextureComponent("notReal.bmp");
            collision = new CollisionComponent();
            rigidBody = new RigidBodyComponent();

            int box3Ref = EntityManager.entityRefFromID(box3Handle);

            EntityManager.Entities[box3Ref].addComponent(transform);
            EntityManager.Entities[box3Ref].addComponent(texture);
            EntityManager.Entities[box3Ref].addComponent(rigidBody);
            EntityManager.Entities[box3Ref].addComponent(collision);
        }

        public void update(FrameEventArgs e)
        {
            getMovement();

            foreach(Entity ent in EntityManager.Entities)
            {
                if(ent.Handle == boxHandle)
                {
                    int boxRef = EntityManager.entityRefFromID(boxHandle);
                    int transformRef = EntityManager.Entities[boxRef].compRefFromType(ComponentType.Transform);

                    ((TransformComponent)ent.Components[transformRef]).addToPosition(movement * (float)e.Time);
                }
            }
        }

        private void getMovement()
        {
            float movementSpeed = 200f;

            KeyboardState keyState = Keyboard.GetState();

            movement = Vector2.Zero;

            if (keyState.IsKeyDown(Key.Up))
            {
                movement += new Vector2(0, movementSpeed);
            }
            if (keyState.IsKeyDown(Key.Down))
            {
                movement += new Vector2(0, -movementSpeed);
            }
            if (keyState.IsKeyDown(Key.Right))
            {
                movement += new Vector2(movementSpeed, 0);
            }
            if (keyState.IsKeyDown(Key.Left))
            {
                movement += new Vector2(-movementSpeed, 0);
            }
        }

        public void onKeyDown(KeyboardKeyEventArgs e)
        {
            if(e.Key == Key.Number1)
            {
                sceneManager.setScene(new SceneDefault(sceneManager));
            }
        }

        public void render(FrameEventArgs e)
        {

        }
    }
}
