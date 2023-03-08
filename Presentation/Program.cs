using Core.Constants;
using Core.Helpers;
using Data;
using Data.Repositories.Concrete;
using Presentation.Services;
using System;
using System.Text;

namespace Presentation
{

    public static class Program
    {
        private readonly static OwnerService _ownerService;
        private readonly static DrugStoreService _drugStoreService;
        private readonly static DruggistService _druggistService;
        private readonly static DrugService _drugService;
        private readonly static AdminService _adminService;

        static Program()
        {
            DbInitialiizer.SeedAdmins();

            _ownerService = new OwnerService();
            _drugStoreService = new DrugStoreService();
            _druggistService = new DruggistService();
            _drugService = new DrugService();
            _adminService = new AdminService();
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

        Authorize: var admin = _adminService.Authorize();

            if (admin is not null)
            {
                ConsoleHelper.WriteWithColor("\n --- Welcome --- ", ConsoleColor.DarkCyan);
            MainMenuDesc: ConsoleHelper.WriteWithColor("\n1. Owners", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("2. Drugstores", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("3. Druggists ", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("4. Drugs", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("0. Exit", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("--- Choose option ---", ConsoleColor.DarkCyan);

                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);
                    goto MainMenuDesc;
                }

                switch (number)
                {
                    case (int)InterfaceConstants.Owners:
                        while (true)
                        {
                        OwnerMenuDesc: ConsoleHelper.WriteWithColor("1. Create Owner", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("2. Update Owner", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("3. Delete Owner", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("4. Get all Owner", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("0. Back to main menu", ConsoleColor.DarkYellow);

                            ConsoleHelper.WriteWithColor("--- Choose option ---", ConsoleColor.DarkCyan);

                            isSucceeded = int.TryParse(Console.ReadLine(), out number);
                            if (!isSucceeded)
                            {
                                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                                goto OwnerMenuDesc;
                            }

                            switch (number)
                            {
                                case (int)OwnerConstants.Create:
                                    _ownerService.Create();
                                    break;
                                case (int)OwnerConstants.Update:
                                    _ownerService.Update();
                                    break;
                                case (int)OwnerConstants.Delete:
                                    _ownerService.Delete();
                                    break;
                                case (int)OwnerConstants.GetAll:
                                    _ownerService.GetAll();
                                    break;

                                case (int)OwnerConstants.GoBack:
                                    goto MainMenuDesc;
                                default:
                                    ConsoleHelper.WriteWithColor("Please try again your choise is not correct", ConsoleColor.Red);
                                    goto OwnerMenuDesc;
                            }
                        }

                    case (int)InterfaceConstants.DrugStores:
                        while (true)
                        {
                        DrugStoreDesc: ConsoleHelper.WriteWithColor("1. Create DrugStore", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("2. Update DrugStore", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("3. Delete DrugStore", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("4. Get all DrugStore", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("5. Get all drugstores by owner", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("0. Back to main menu", ConsoleColor.DarkYellow);


                            ConsoleHelper.WriteWithColor("--- Choose option ---", ConsoleColor.DarkCyan);
                            isSucceeded = int.TryParse(Console.ReadLine(), out number);
                            if (!isSucceeded)
                            {
                                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                                goto DrugStoreDesc;
                            }

                            switch (number)
                            {
                                case (int)DrugStoreConstants.Create:
                                    _drugStoreService.Create();
                                    break;

                                case (int)DrugStoreConstants.Update:
                                    _drugStoreService.Update();
                                    break;

                                case (int)DrugStoreConstants.Delete:
                                    _drugStoreService.Delete();
                                    break;

                                case (int)DrugStoreConstants.GetAll:
                                    _drugStoreService.GetAll();
                                    break;
                                case (int)DrugStoreConstants.GetAllDrugstoresByOwner:
                                    _drugStoreService.GetAllDrugstoresByOwner();
                                    break;

                                case (int)DrugStoreConstants.GoBack:
                                    goto MainMenuDesc;

                            }
                        }

                    case (int)InterfaceConstants.Druggists:
                        while (true)
                        {
                        DruggistMenuDesc: ConsoleHelper.WriteWithColor("1. Create Druggist", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("2. Update Druggist", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("3. Delete Druggist", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("4. Get all Druggist", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("0. Back to main menu", ConsoleColor.DarkYellow);

                            ConsoleHelper.WriteWithColor("--- Choose option ---", ConsoleColor.DarkCyan);

                            isSucceeded = int.TryParse(Console.ReadLine(), out number);
                            if (!isSucceeded)
                            {
                                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                                goto DruggistMenuDesc;
                            }

                            switch (number)
                            {
                                case (int)DruggistConstants.Create:
                                    _druggistService.Create();
                                    break;

                                case (int)DruggistConstants.Update:
                                    _druggistService.Update();
                                    break;

                                case (int)DruggistConstants.Delete:
                                    _druggistService.Delete();
                                    break;

                                case (int)DruggistConstants.GetAll:
                                    _druggistService.GetAll();
                                    break;

                                case (int)DruggistConstants.GoBack:
                                    goto MainMenuDesc;

                                default:
                                    ConsoleHelper.WriteWithColor("Please try again your choise is not correct", ConsoleColor.Red);
                                    goto DruggistMenuDesc;
                            }
                        }
                        break;


                    case (int)InterfaceConstants.Drugs:
                        while (true)
                        {
                        DrugsMenuDesc: ConsoleHelper.WriteWithColor("1. Create Drug", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("2. Update Drug", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("3. Delete Drug", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("4. Get all Drug", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("5. GetAllDrugsByDrugstore", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("6. Filter", ConsoleColor.DarkYellow);
                            ConsoleHelper.WriteWithColor("0. Back to main menu", ConsoleColor.DarkYellow);

                            ConsoleHelper.WriteWithColor("--- Choose option ---", ConsoleColor.DarkCyan);

                            isSucceeded = int.TryParse(Console.ReadLine(), out number);
                            if (!isSucceeded)
                            {
                                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                                goto DrugsMenuDesc;
                            }

                            switch (number)
                            {
                                case (int)DrugConstants.Create:
                                    _drugService.Create();
                                    break;

                                case (int)DrugConstants.Update:
                                    _drugService.Update();
                                    break;

                                case (int)DrugConstants.Delete:
                                    _drugService.Delete();
                                    break;

                                case (int)DrugConstants.GetAll:
                                    _drugService.GetAll();
                                    break;

                                case (int)DrugConstants.GetAllDrugsByDrugstores:
                                    _drugService.GetAllDrugsByDrugstore();
                                    break;

                                case (int)DrugConstants.Filter:
                                    _drugService.Filter();
                                    break;

                                case (int)DrugConstants.GoBack:
                                    goto MainMenuDesc;

                                default:
                                    ConsoleHelper.WriteWithColor("Please try again your choise is not correct", ConsoleColor.Red);
                                    goto DrugsMenuDesc;
                            }
                        }
                    case (int)InterfaceConstants.Exit:
                        goto Authorize;
                    default:
                        ConsoleHelper.WriteWithColor("Please try again your choise is not correct", ConsoleColor.Red);
                        goto MainMenuDesc;
                }
            }




        }
    }
}

