using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserConflictException : ConflictException
    {
        public UserConflictException(string username) : base($"User with name: {username} exists yet")
        {

        }
    }
}
