﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class Brand :BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Model> Models { get; set; }
    }
}
