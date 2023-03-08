using Core1.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DrugStore : BaseEntities
    {
        public string Address { get; set; }
        public int ContactNumber { get; set; }
        public int Email { get; set; }
        public string Druggists { get; set; }
        public string Drugs { get; set; }
        public string Owner { get; set; }


    }
}
