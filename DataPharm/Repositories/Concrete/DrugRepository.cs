using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class DrugRepository : IDrugRepository
    {
        static int id;
        public List<Drug> GetAll()
        {
            return DbContext.Drugs;
        }

        public Drug Get(int id)
        {
            return DbContext.Drugs.FirstOrDefault(g => g.Id == id);
        }

        public void Add(Drug drug)
        {
            id++;
            drug.Id = id;
            DbContext.Drugs.Add(drug);
        }

        public void Update(Drug drug)
        {
            var DbDrug = DbContext.Drugs.FirstOrDefault(g => g.Id == id);
            if (DbDrug is not null)
            {
                DbDrug.Name = drug.Name;
                DbDrug.Price = drug.Price;
                DbDrug.Count = drug.Count;
                DbDrug.DrugStore = drug.DrugStore;
            }
        }

        public void Delete(Drug drug)
        {
            DbContext.Drugs.Remove(drug);
        }

        public List<Drug> GetDrugsByPrice(double price)
        {
            return DbContext.Drugs.Where(d => d.Price <= price).ToList();
        }
    }
}
