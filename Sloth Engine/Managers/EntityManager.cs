using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Components;
using Sloth_Engine.Objects;

namespace Sloth_Engine.Managers
{
    public static class EntityManager
    {
        private static List<Entity> entities = new List<Entity>();

        public static List<Entity> Entities => entities;

        public static void addEntity(Entity pEntity)
        {
            Entities.Add(pEntity);
        }

        public static void nukeEntities()
        {
            entities.Clear();
            GC.Collect();
        }

        public static Guid createNewBlankEntity()
        {
            Entity newEnt = new Entity();
            entities.Add(newEnt);
            return newEnt.Handle;
        }

        public static int entityRefFromID(Guid id)
        {
            int result = -1;

            for(int i =0; i < entities.Count; i++)
            {
                if(entities[i].Handle == id)
                {
                    result = i;
                }
            }

            if(result == -1)
            {
                throw new ApplicationException("No Entity with ID " + id.ToString() + " found");
            }

            return result;
        }
    }
}
