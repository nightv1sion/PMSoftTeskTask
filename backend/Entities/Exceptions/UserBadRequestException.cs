using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException() : base($"Wrong username or password")
        {
        }
    }
}
