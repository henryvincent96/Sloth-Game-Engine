using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloth_Engine.Components;

namespace Sloth_Engine.Objects
{
    public class Entity
    {
        private List<Component> components;
        private Guid handle;

        public Entity()
        {
            handle = Guid.NewGuid();
            components = new List<Component>();
        }

        public Guid Handle
        {
            get { return handle; }
        }

        public List<Component> Components
        {
            get { return components; }
        }

        public void addComponent(Component component)
        {
            if (hasComponentWithType(component.Type))
            {
                throw new ApplicationException("Component with type " + component.Type +
                    " already exists within entity " + handle.ToString());
            }
            else
            {
                component.EntityHandle = handle;
                components.Add(component);
            }
        }

        public bool hasComponentWithType(ComponentType type)
        {
            bool result = false;

            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].Type == type)
                {
                    result = true;
                }
            }

            return result;
        }

        public int compRefFromType(ComponentType type)
        {
            int result = -1;

            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].Type == type)
                {
                    result = i;
                }
            }

            if(result == -1)
            {
                throw new ApplicationException("No component with type " + type.ToString() +
                    " found within entity " + handle.ToString());
            }

            return result;
        }

        /// <summary>
        /// Removes handle of a given component ID
        /// </summary>
        /// <param name="pID"></param>
        public void removeComponent(Component pComponent)
        {
            components.RemoveAt(getIndexofID(pComponent.Handle));
        }
        
        /// <summary>
        /// Gets the index of an ID
        /// Throws exception if ID not found
        /// </summary>
        /// <param name="pID">Index to be found</param>
        /// <returns>Index of ID</returns>
        private int getIndexofID(Guid pID)
        {
            int idIndex = -1;

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Handle == pID)
                {
                    idIndex = i;
                }
            }

            if (idIndex == -1)
            {
                throw new ApplicationException("No component with ID '" + pID + "' found");
            }

            return idIndex;
        }

        public bool Equals(Entity entity)
        {
            if(handle == entity.Handle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
