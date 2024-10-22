﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
