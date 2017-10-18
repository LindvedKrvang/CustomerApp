using System;
using System.Collections.Generic;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;

namespace CustomerAppUI
{
    class Program
    {
        static BLLFacade _bllFacade = new BLLFacade();
        private static bool _programIsRunning;

        static void Main(string[] args)
        {
            string[] menuItems =
            {
                "List All Customers",
                "Add Customer",
                "Delete Customer",
                "Edit Customer",
                "Exit"
            };
            _programIsRunning = true;

            do
            {
                var selected = ShowMenu(menuItems);
                HandleSelectedMenuOption(selected);
            } while (_programIsRunning);
        }

        private static int ShowMenu(string[] menuItems)
        {
            //Console.Clear();
            Console.WriteLine("Choose an option:\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{i + 1} : {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 5)
            {
                Console.WriteLine("You need to select a number between 1 and 5");
            }
            return selection;
        }

        private static void HandleSelectedMenuOption(int selection)
        {
            switch (selection)
            {
                case 1:
                {
                    ListCustomers();
                    Console.ReadLine();
                    break;
                }
                case 2:
                {
                    AddCustomer();
                    Console.ReadLine();
                    break;
                }
                case 3:
                {
                    DeleteCustomer();
                    Console.ReadLine();
                    break;
                }
                case 4:
                {
                    EditCustomer();
                    Console.ReadLine();
                    break;
                }
                case 5:
                {
                    Console.WriteLine("Exiting program");
                    _programIsRunning = false;
                    break;
                }
            }
        }

        private static void EditCustomer()
        {
            var customerToEdit = FindCustomerById();
            if (customerToEdit == null)
            {
                Console.WriteLine("That customer doesn't exist.");
                return;
            }
            Console.WriteLine("Firstname: ");
            customerToEdit.FirstName = Console.ReadLine();
            Console.WriteLine("LastName:");
            customerToEdit.LastName = Console.ReadLine();
            Console.WriteLine("Address: ");
            customerToEdit.Address = Console.ReadLine();

            _bllFacade.CustomerService.UpdateCustomer(customerToEdit);
        }

        private static CustomerBO FindCustomerById()
        {
            Console.WriteLine("Insert Customer Id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number!");
            }
            return _bllFacade.CustomerService.GetCustomer(id);
        }

        private static void DeleteCustomer()
        {
            var customerToDelete = FindCustomerById();
            if (customerToDelete == null)
            {
                Console.WriteLine("That customer doesn't exist.");
                return;
            }
            _bllFacade.CustomerService.DeleteCustomer(customerToDelete.Id);
        }

        private static void AddCustomer()
        {
            Console.WriteLine("Firstname:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Lastname:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Address:");
            var address = Console.ReadLine();

            _bllFacade.CustomerService.CreateCustomer(new CustomerBO()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            });
        }

        private static void ListCustomers()
        {
            Console.WriteLine("\nList of Customers:");
            foreach (var customer in _bllFacade.CustomerService.GetAllCustomers())
            {
                Console.WriteLine(
                    $"Id: {customer.Id} \nName: {customer.FullName} \nAddress: {customer.Address}");
                Console.WriteLine();
            }
        }
    }
}