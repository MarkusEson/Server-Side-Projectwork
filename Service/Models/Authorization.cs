﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Authorization
    {
        public enum Rank {
            user = 0,
            administrator = 1
        };
    }
}