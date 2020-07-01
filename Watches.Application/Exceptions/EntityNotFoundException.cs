using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int id, Type type)
            :base($"Entity with type of {type.Name} with an id of {id} was not found.")
        {

        }
    }
}
