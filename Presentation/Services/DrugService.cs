using Core.Entities;
using Core.Helper;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Linq;

namespace Presentation.Services
{
    public class DrugService
    {
        private readonly DruggistService _druggistService;
        private readonly DrugStoreRepository _drugStoreRepository;
        private readonly DrugStoreService _drugStoreService;
        private readonly DrugRepository _drugRepository;
        public DrugService()
        {
            _druggistService = new DruggistService();
            _drugStoreRepository = new DrugStoreRepository();
            _drugStoreService = new DrugStoreService();
            _drugRepository = new DrugRepository();
        }

        public void Create()
        {
            if (_drugStoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create DrugStore first");
                return;
            }
            ConsoleHelper.WriteWithColor("Enter Drug name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
        EnterPrice: ConsoleHelper.WriteWithColor("Enter Drug price", ConsoleColor.Cyan);
            int price;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out price);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed price is not correct format", ConsoleColor.Red);
                goto EnterPrice;
            }
            ConsoleHelper.WriteWithColor("Enter Drug count", ConsoleColor.Cyan);
            int count;
            isSucceeded = int.TryParse(Console.ReadLine(), out count);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed count is not correct format", ConsoleColor.Red);
                goto EnterPrice;
            }

            _drugStoreService.GetAll();
        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter DrugStore Id");
            int drugStoreId;
            isSucceeded = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var drugStore = _drugStoreRepository.Get(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not exist", ConsoleColor.Red);
                goto EnterIdDesc;
            }



            var drug = new Drug
            {
                Name = name,
                Price = price,
                Count = count,
                Drugstore = drugStore
            };
            _drugRepository.Add(drug);
            drug.Drugstore.Drugs.Add(drug);



        }



        public void GetAll()
        {
            var drugs = _drugRepository.GetAll();

            ConsoleHelper.WriteWithColor("--- All Drugs ---", ConsoleColor.Cyan);

            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"Id : {drug.Id} \nName : {drug.Name} \nPrice : {drug.Price} \nCount : {drug.Count} \n DrugStore : {drug.DrugStore.Name}", ConsoleColor.Blue);
            }
        }

        public void GetAllDrugsByDrugstore()
        {
            var drugstore = _drugStoreRepository.GetAll();
            if (drugstore.Count == 0)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no Drugs to show in database\n Press any key to continue", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }
        drugStoreIdCheck:
            foreach (var drug in drugstore)
            {
                ConsoleHelper.WriteWithColor($"Id : {drug.Id} \nFullname : {drug.Name}");
            }

            ConsoleHelper.WriteWithColor("Enter Drug Id", ConsoleColor.Blue);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Wrong Id input format\n Please choose id from the list\nPress any key to continue ", ConsoleColor.Red);
                Console.ReadKey();
                goto drugStoreIdCheck;
            }

            var drugs = _drugRepository.GetAll();
            if (drugs.Count == 0)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no drug in this drugstore\n Please choose from the list\nPress any key to continue", ConsoleColor.Red);
                Console.ReadKey();
            }
            Console.Clear();
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"Id : {drug.Id}\nName :{drug.Name}\n Count : {drug.Count} ", ConsoleColor.Green);
            }
            ConsoleHelper.WriteWithColor("Press any key to continue\n", ConsoleColor.Green);
            Console.ReadKey();
        }

        public void Update()
        {
            GetAll();

            if (_drugRepository.GetAll().Count is 0)
            {
                return;
            }
        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter Id for Updating or press to 0 back to menu", ConsoleColor.DarkCyan);
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

            Drug drug = _drugRepository.Get(id);
            if (drug is null)
            {
                ConsoleHelper.WriteWithColor("There is no any owner in this Id", ConsoleColor.Red);
                return;
            }

            ConsoleHelper.WriteWithColor("Enter new Drug name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
        EnterPrice: ConsoleHelper.WriteWithColor("Enter new Drug price", ConsoleColor.Cyan);
            int price;
            isSucceeded = int.TryParse(Console.ReadLine(), out price);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed price is not correct format", ConsoleColor.Red);
                goto EnterPrice;
            }
            ConsoleHelper.WriteWithColor("Enter new Drug count", ConsoleColor.Cyan);
            int count;
            isSucceeded = int.TryParse(Console.ReadLine(), out count);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed count is not correct format", ConsoleColor.Red);
                goto EnterPrice;
            }
            if (count <= 0)
            {
                ConsoleHelper.WriteWithColor("Inputed count must be bigger than 0", ConsoleColor.Red);
            }

            _drugStoreService.GetAll();
            ConsoleHelper.WriteWithColor("Enter new DrugStore Id");
            int drugStoreid;
            isSucceeded = int.TryParse(Console.ReadLine(), out drugStoreid);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var DrugStore = _drugStoreRepository.Get(drugStoreid);
            if (DrugStore is null)
            {
                ConsoleHelper.WriteWithColor("Inputed Id is not exist", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            drug.Name = name;
            drug.Price = price;
            drug.Count = count;

            drug.Drugstore = DrugStore;

            DrugStore.Drugs.Add(drug);
            _drugRepository.Add(drug);
            ConsoleHelper.WriteWithColor($"{drug.Name}  is succesfully updated", ConsoleColor.Green);

        }


        public void Delete()
        {
            GetAll();

            if (_drugRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is not any drug", ConsoleColor.Red);
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

            Drug Dbdrug = _drugRepository.Get(id);
            if (Dbdrug is null)
            {
                ConsoleHelper.WriteWithColor("There is no owner in this Id", ConsoleColor.Red);
                return;
            }
            _drugRepository.Delete(Dbdrug);
            ConsoleHelper.WriteWithColor("DrugStore is succesfully deleted", ConsoleColor.Green);
        }


        public void Filter()
        {
        EnterPrice: ConsoleHelper.WriteWithColor("Enter price", ConsoleColor.Cyan);
            double price;
            bool isSucceeded = double.TryParse(Console.ReadLine(), out price);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed price is not correct format", ConsoleColor.Red);
                goto EnterPrice;
            }

            var drugs = _drugRepository.GetDrugsByPrice(price);
            if (drugs.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any drug", ConsoleColor.Red);
            }

            foreach (var drug in drugs)
            {

                ConsoleHelper.WriteWithColor($"ID:{drug.Id} Name:{drug.Name} Price:{drug.Price} Count:{drug.Count} DrugStore:{drug.DrugStore.Name}");
            }

        }
    }
}