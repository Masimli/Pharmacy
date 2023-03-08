using Core.Entities;
using Core.Extension;
using Core.Helper;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using System;


namespace Presentation.Services
{
    public class OwnerService
    {
        private readonly OwnerRepository _ownerRepository;
        public OwnerService()
        {
            _ownerRepository = new OwnerRepository();

        }
        public void GetAll()
        {
            var owners = _ownerRepository.GetAll();

            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Owner", ConsoleColor.Red);
                return;
            }

            ConsoleHelper.WriteWithColor("--- All Owners ---", ConsoleColor.Cyan);

            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Id : {owner.Id} \nName : {owner.Name} \nSurname : {owner.Surname} ", ConsoleColor.Blue);
            }
        }
        public void Delete()
        {

            GetAll();

        EnterIdDescription: ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);
            int id;
            bool isSecceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSecceeded)
            {
                ConsoleHelper.WriteWithColor("Enter id for deleting or press to 0 for back to menu  ", ConsoleColor.Red);
                goto EnterIdDescription;
            }
            else if (id == 0)
            {
                return;
            }

            var owner = _ownerRepository.Get(id);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Owner By This ID ", ConsoleColor.Red);
            }
            else
            {
                _ownerRepository.Delete(owner);
                ConsoleHelper.WriteWithColor($"{owner.Name} {owner.Surname} is succesfuly deleted ", ConsoleColor.Green);
            }
        }
        public void Create()
        {

            ConsoleHelper.WriteWithColor("--- Enter Owner Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Owner Surname ---", ConsoleColor.DarkCyan);
            string surname = Console.ReadLine();


            var owners = new Owner
            {
                Name = name,
                Surname = surname
            };

            _ownerRepository.Add(owners);
            ConsoleHelper.WriteWithColor($"{owners.Name} {owners.Surname} is succecfully created", ConsoleColor.Green);
        }
        public void Update()
        {
            GetAll();

            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Owner for update", ConsoleColor.Red);
                return;
            }


        UpdatıngDesc: ConsoleHelper.WriteWithColor("Enter Owner Id For Updating or press to 0 for return menu", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id Is Not Correct Format", ConsoleColor.Red);
                goto UpdatıngDesc;
            }
            else if (id == 0)
            {
                return;
            }
            var owner = _ownerRepository.Get(id);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("There Is No Any Owner In This Id", ConsoleColor.Red);
                goto UpdatıngDesc;
            }
            ConsoleHelper.WriteWithColor("Enter New Name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter New Surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

            owner.Name = name;
            owner.Surname = surname;

            _ownerRepository.Update(owner);
            ConsoleHelper.WriteWithColor("Owner Is Succesfully Updating", ConsoleColor.Green);
        }



    }

}

