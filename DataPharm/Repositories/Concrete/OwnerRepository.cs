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
    public class OwnerRepository : IOwnerRepository
    {
        static int id;

        public List<Owner> GetAll()
        {
            return DbContext.Owners;
        }

        public Owner Get(int id)
        {
            return DbContext.Owners.FirstOrDefault(g => g.Id == id);
        }

        public void Add(Owner owner)
        {
            id++;
            owner.Id = id;
            DbContext.Owners.Add(owner);
        }

        public void Update(Owner owner)
        {
            var DbOwner = DbContext.Owners.FirstOrDefault(g => g.Id == id);
            if (DbOwner is not null)
            {
                DbOwner.Name = owner.Name;
                DbOwner.Surname = owner.Surname;
            }
        }

        public void Delete(Owner owner)
        {
            DbContext.Owners.Remove(owner);
        }



    }
}
