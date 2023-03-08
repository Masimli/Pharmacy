using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public static class DbContext
    {
        static DbContext()
        {
            Admins = new List<Admin>();
            Owners = new List<Owner>();
            Drugstores = new List<DrugStore>();
            Druggists = new List<Druggist>();
            Drugs = new List<Drug>();
        }
        public static List<Owner> Owners { get; set; }
        public static List<DrugStore> Drugstores { get; set; }
        public static List<Druggist> Druggists { get; set; }
        public static List<Drug> Drugs { get; set; }
        public static List<Admin> Admins { get; set; }
    }
}
