using System;

namespace qwertygroup.Core.Models
{
    public abstract class MyIdentifyable
    {
        /**
        This class mostly exists because i did not want to write this a billion times
        */
        public virtual void CheckId(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new InvalidOperationException(message);
            }
            if(id>int.MaxValue){
                throw new InvalidOperationException();
            }
        }
    }
}