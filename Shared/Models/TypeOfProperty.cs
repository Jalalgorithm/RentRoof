﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.Models
{
    public class TypeOfProperty
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public List<House> Houses { get; set; }
    }
}
