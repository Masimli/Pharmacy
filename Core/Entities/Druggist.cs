﻿using Core1.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Druggist : BaseEntities
    {
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public string DrugStore { get; set; }
    }
}
