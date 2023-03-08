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
    public class DrugStoreRepository : IDrugstoreRepository
    {
        static int id;
        public List<Drugstore> GetAll()
        {
            return DbContext.Drugstores;
        }
        public Drugstore Get(int id)
        {
            return DbContext.Drugstores.FirstOrDefault(d => d.Id == id);
        }
        public void Add(Drugstore drugstore)
        {
            id++;
            drugstore.Id = id;
            DbContext.Drugstores.Add(drugstore);
        }
        public void Update(Drugstore drugstore)
        {
            var DbDrugStore = DbContext.Drugstores.FirstOrDefault(d => d.Id == id);
            if (DbDrugStore is not null)
            {
                DbDrugStore.Name = drugstore.Name;
                DbDrugStore.Address = drugstore.Address;
                DbDrugStore.ContactNumber = drugstore.ContactNumber;
                DbDrugStore.Email = drugstore.Email;
                DbDrugStore.Druggists = drugstore.Druggists;
                DbDrugStore.Drugs = drugstore.Drugs;
            }
        }

        public void Delete(Drugstore drugstore)
        {
            DbContext.Drugstores.Remove(drugstore);
        }

        public bool IsDuplicatedEmail(string email)
        {
            return DbContext.Drugstores.Any(e => e.Email == email);
        }
    }
}
