﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
