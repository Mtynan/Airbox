﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserLocation : EntityBase
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
