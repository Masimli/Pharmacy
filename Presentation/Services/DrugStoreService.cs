using Core.Entities;
using Core.Extension;
using Core.Helper;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class DrugStoreService
    {
        private readonly DrugStoreRepository _drugStoreRepository;
        private readonly OwnerRepository _ownerRepository;
        private readonly OwnerService _ownerService;
        public DrugStoreService()
        {
            _drugStoreRepository = new DrugStoreRepository();
            _ownerRepository = new OwnerRepository();
            _ownerService = new OwnerService();
        }
        public void Create()
        {
            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create owner for create Drugstore", ConsoleColor.DarkCyan);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter Drugstore Name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter Drugstore Address", ConsoleColor.Cyan);
            string address = Console.ReadLine();
        EmailDesc: ConsoleHelper.WriteWithColor("Enter Drugstore Email", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format!", ConsoleColor.Red);
                goto EmailDesc;
            }

            if (_drugStoreRepository.IsDuplicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email already used!", ConsoleColor.Red);
                goto EmailDesc;
            }
            ConsoleHelper.WriteWithColor("Enter Drugstore ContactNumber", ConsoleColor.Cyan);
            string ContactNumber = Console.ReadLine();

        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter Owner Id", ConsoleColor.Cyan);
            int ownerid;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out ownerid);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var owner = _ownerRepository.Get(ownerid);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not exist", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            var drugStore = new DrugStore
            {
                Name = name,
                Address = address,
                Email = email,
                Owner = owner,
                ContactNumber = ContactNumber
            };
            owner.Drugstores.Add(drugStore);
            _drugStoreRepository.Add(drugStore);
            ConsoleHelper.WriteWithColor($"{drugStore.Name} drugstore is succesfully created", ConsoleColor.Green);
        }
        public void GetAll()
        {
            var drugStores = _drugStoreRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Drug Store --- ");
            if (drugStores.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Drugstore", ConsoleColor.Red);
            }
            foreach (var drugStore in drugStores)
            {
                ConsoleHelper.WriteWithColor($"ID:{drugStore.Id}\n Name:{drugStore.Name}\n Email:{drugStore.Email}", ConsoleColor.DarkMagenta);
            }

        }
        public void Delete()
        {
            GetAll();

            if (_drugStoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is not any Drugstore ", ConsoleColor.Red);
                return;
            }
        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter Id for deleting press to 0 for back to menu", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            else if (id == 0)
            {
                return;
            }

            Drugstore DbdrugStore = _drugStoreRepository.Get(id);
            if (DbdrugStore is null)
            {
                ConsoleHelper.WriteWithColor("There is no owner in this Id", ConsoleColor.Red);
                return;
            }
            _drugStoreRepository.Delete(DbdrugStore);
            ConsoleHelper.WriteWithColor("Drugstore is succesfully deleted", ConsoleColor.Green);
        }
        public void Update()
        {
            GetAll();

            if (_drugStoreRepository.GetAll().Count == 0)
            {
                return;
            }
        UpdatingDesc: ConsoleHelper.WriteWithColor("Enter DrugStore Id for updating or press to 0 for back to menu", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                goto UpdatingDesc;
            }
            else if (id == 0)
            {
                return;
            }
            var drugStore = _drugStoreRepository.Get(id);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("There is no any DrugStore in this Id", ConsoleColor.Red);
                goto UpdatingDesc;
            }
            ConsoleHelper.WriteWithColor("Enter new name");
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter new DrugStore address", ConsoleColor.Cyan);
            string address = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter new DrugStore contact number", ConsoleColor.Cyan);
            string contactnumber = Console.ReadLine();
        EmailDesc: ConsoleHelper.WriteWithColor("Enter new Drugstore email", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format", ConsoleColor.Red);
                goto EmailDesc;
            }

            if (_drugStoreRepository.IsDuplicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email already used", ConsoleColor.Red);
                goto EmailDesc;
            }
            _ownerService.GetAll();
        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter new owner Id", ConsoleColor.Cyan);
            int ownerid;
            isSucceeded = int.TryParse(Console.ReadLine(), out ownerid);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var owner = _ownerRepository.Get(ownerid);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not exist", ConsoleColor.Red);
                goto EnterIdDesc;
            }


            drugStore.Name = name;
            drugStore.Email = email;
            drugStore.Address = address;
            drugStore.ContactNumber = contactnumber;
            drugStore.Owner = owner;


            _drugStoreRepository.Update(drugStore);
            ConsoleHelper.WriteWithColor("Drugstore is succesfully updating", ConsoleColor.Green);
        }
        public void GetAllDrugstoresByOwner()
        {
            var owners = _ownerRepository.GetAll();
            if (owners.Count == 0)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no Groups to show in database\n Press any key to continue", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }
        drugStoreIdCheck:
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Id: {owner.Id} \n Name: {owner.Name}\n Surname: {owner.Surname}");
            }

            ConsoleHelper.WriteWithColor("Enter Owner Id", ConsoleColor.Blue);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Wrong Id input format\n Please choose id from the list\nPress any key to continue ", ConsoleColor.Red);
                Console.ReadKey();
                goto drugStoreIdCheck;
            }

            var Storeowner = _ownerRepository.Get(id);
            if (Storeowner == null)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no owner with this id\n Please choose from the list\nPress any key to continue", ConsoleColor.Red);
                Console.ReadKey();
            }
            Console.Clear();
            foreach (var drugstore in Storeowner.Drugstores)
            {
                ConsoleHelper.WriteWithColor($"Id: {drugstore.Id}\nName: {drugstore.Name}\n DrugstoreName: {drugstore.Name}", ConsoleColor.Green);
            }
            ConsoleHelper.WriteWithColor("Press any key to continue\n", ConsoleColor.Green);
            Console.ReadKey();
        }
    }
}
