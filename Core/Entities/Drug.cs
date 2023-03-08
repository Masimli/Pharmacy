using Core1.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Drug : BaseEntities
    {
        public double Price { get; set; }
        public int Count { get; set; }
        public string DrugStore { get; set; } 
    }
}
