using System;

namespace qwertygroup.Core.Models
{
    public abstract class MyIdentifyable
    {
        public virtual void CheckId(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new InvalidOperationException(message);
            }
        }
    }
}