using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Scenes;
using Sloth_Engine.Objects;
using Sloth_Engine.Components;
using Sloth_Engine.Managers;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Input;

namespace MyGame
{
    public class SceneDefault : Scene, IScene
    {
        Guid fishHandle, bubbleHandle, plantsHandle;
        Vector2 movement;
        SceneManager sceneManager;

        public SceneDefault(SceneManager inSceneManager) : base(inSceneManager)
        {
            sceneManager = inSceneManager;

            sceneManager.updater = update;
            sceneManager.renderer = render;
            sceneManager.keyDowner = onKeyDown;

            GL.ClearColor(Color4.CornflowerBlue);

            TransformComponent randTrans;
            TextureComponent randTex;
            CollisionComponent randColl;
            RigidBodyComponent randRig;

            fishHandle = EntityManager.createNewBlankEntity();

            randTrans = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(-450, 0),
                Scale = new Vector2(2f, 0.912f)
            };
            randTex = new TextureComponent("Fish1.png");
            randRig = new RigidBodyComponent()
            {
                Gravity = new Vector2(0.4f, 0f)
            };
            randColl = new CollisionComponent();

            EntityManager.Entities[EntityManager.entityRefFromID(fishHandle)].addComponent(randTrans);
            EntityManager.Entities[EntityManager.entityRefFromID(fishHandle)].addComponent(randTex);
            EntityManager.Entities[EntityManager.entityRefFromID(fishHandle)].addComponent(randColl);
            EntityManager.Entities[EntityManager.entityRefFromID(fishHandle)].addComponent(randRig);

            bubbleHandle = EntityManager.createNewBlankEntity();

            randTrans = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(250, -375),
                Scale = new Vector2(0.5f, 0.5f)
            };
            randTex = new TextureComponent("Bubble1.png");
            randColl = new CollisionComponent();
            randRig = new RigidBodyComponent()
            {
                Gravity = new Vector2(0, 0.6f)
            };
            AudioComponent bubblePopSound = new AudioComponent("bubblepop.wav");

            int entityRef = EntityManager.entityRefFromID(bubbleHandle);

            EntityManager.Entities[entityRef].addComponent(randTrans);
            EntityManager.Entities[entityRef].addComponent(randTex);
            EntityManager.Entities[entityRef].addComponent(randColl);
            EntityManager.Entities[entityRef].addComponent(randRig);
            EntityManager.Entities[entityRef].addComponent(bubblePopSound);

            plantsHandle = EntityManager.createNewBlankEntity();

            randTrans = new TransformComponent(Matrix4.Identity)
            {
                Position = new Vector2(0, -265),
                Scale = new Vector2(11.8f, 1.5f)
            };
            randTex = new TextureComponent("Plants.png");

            EntityManager.Entities[EntityManager.entityRefFromID(plantsHandle)].addComponent(randTrans);
            EntityManager.Entities[EntityManager.entityRefFromID(plantsHandle)].addComponent(randTex);
        }

        public void update(FrameEventArgs e)
        {
            foreach (Entity ent in EntityManager.Entities)
            {
                if(ent.Handle == fishHandle)
                {
                    getMovement();

                    int transformRef = ent.compRefFromType(ComponentType.Transform);

                    if(movement.X < 0)
                    {
                        Vector2 currentScale = ((TransformComponent)ent.Components[transformRef]).Scale;
                        ((TransformComponent)ent.Components[transformRef]).Scale = new Vector2(-currentScale.X, currentScale.Y);
                    }
                    else
                    {
                        Vector2 currentScale = ((TransformComponent)ent.Components[transformRef]).Scale;
                        ((TransformComponent)ent.Components[transformRef]).Scale = new Vector2(currentScale.X, currentScale.Y);
                    }

                    ((TransformComponent)ent.Components[transformRef]).addToPosition(movement * (float)e.Time);

                    if(((TransformComponent)ent.Components[transformRef]).Position.X > 675)
                    {
                        float y = ((TransformComponent)ent.Components[transformRef]).Position.Y;
                        ((TransformComponent)ent.Components[transformRef]).Position = new Vector2(-675, y);
                    }
                }

                if(ent.Handle == bubbleHandle)
                {
                    if (((CollisionComponent)ent.Components[ent.compRefFromType(ComponentType.Collision)]).IsCollided)  
                    {
                        Random random = new Random();

                        ((TransformComponent)ent.Components[ent.compRefFromType(ComponentType.Transform)]).Position = new Vector2(random.Next(-450, 450), -375);
                        ((AudioComponent)ent.Components[ent.compRefFromType(ComponentType.Audio)]).Triggered = true;
                    }

                    if (((TransformComponent)ent.Components[ent.compRefFromType(ComponentType.Transform)]).Position.Y > 375)
                    {
                        Random random = new Random();

                        ((TransformComponent)ent.Components[ent.compRefFromType(ComponentType.Transform)]).Position = new Vector2(random.Next(-450, 450), -375);
                    }
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
            if(e.Key == Key.Number2)
            {
                sceneManager.setScene(new Scene2(sceneManager));
            }
        }

        public void render(FrameEventArgs e)
        {

        }
    }
}
