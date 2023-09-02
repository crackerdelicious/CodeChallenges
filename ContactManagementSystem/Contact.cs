using System.Text.RegularExpressions;

namespace ContactManagementSystem
{
    public class Contact
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Contact(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public void DisplayContactDetails()
        {
            Console.Write($"{Id}.");
            Console.WriteLine($" Name: {Name}");
            Console.WriteLine($"   Phone: {Phone}");
            Console.WriteLine($"   Email: {Email}");
            Console.WriteLine();
        }
    }

    public class ContactRepository
    {
        private static int _nextId = 1;
        private static List<Contact> _contacts = new List<Contact>();

        public static void AddContact(string name, string email, string phone)
        {
            int id = _nextId++;
            Contact contact = new Contact(id, name, email, phone);
            _contacts.Add(contact);
        }

        public static List<Contact> GetAllContacts()
        {
            return _contacts;
        }
    }

    public class ContactDisplay
    {
        public static void DisplayContacts(List<Contact> contacts)
        {
            if (contacts.Count != 0)
            {
                Console.WriteLine("List of Contacts:");
                foreach (Contact contact in contacts)
                {
                    contact.DisplayContactDetails();
                }
            }
            else
            {
                Console.WriteLine("No contact in a contact list");
            }
        }
    }

    public class ContactSearch
    {
        public static List<Contact> SearchContactsByName(List<Contact> contacts, string name)
        {
            return contacts.Where(contact => contact.Name.ToLower().Contains(name.ToLower(), StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    public class ContactManager
    {
        private static void InitialContact()
        {
            Console.WriteLine("Application Start...");

            // Add contact Lists
            ContactRepository.AddContact("Alice Brave", "alice@mail.com", "(+66)9-2030-4635");
            ContactRepository.AddContact("Bob Grant", "bob@mail.com", "(+66)9-7232-0280");
            ContactRepository.AddContact("John Doe", "john@mail.com", "(+66)8-1651-4368");
        }

        private static void PrintMenus()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1.Add a new contact");
            Console.WriteLine("2.View all contacts");
            Console.WriteLine("3.Search contacts by name");
            Console.WriteLine("4.Exit");
        }

        private static string CreateContact(string input)
        {
            string? value;
            do
            {
                Console.Write($"Enter the contact's {input.ToLower()}: ");
                value = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"{input} cannot be empty or whitespace.");
                    continue;
                }

                if (input.Equals("Email", StringComparison.OrdinalIgnoreCase) && !IsValidEmail(value))
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email.");
                    continue;
                }

                if (input.Equals("Phone", StringComparison.OrdinalIgnoreCase) && !IsValidPhoneNumber(value))
                {
                    Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                    continue;
                }
                break; // Input is valid, exit the loop
            } while (true);

            Console.Write($"{input} added.\n");
            return value;
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+\d{2,4}-\d{3,4}-\d{4}$|@""^\+\d{2,4}-\d{3}-\d{4}$");
        }

        public static void Run()
        {
            InitialContact();
            Console.WriteLine("Welcome to the Contact Management System!\n");

            int choice;
            do
            {
                PrintMenus();
                Console.Write("Enter choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ContactRepository.AddContact(CreateContact("Name"), CreateContact("Email"), CreateContact("Phone"));
                            Console.WriteLine("\nContact added successfully!\n");
                            break;
                        case 2:
                            if (ContactRepository.GetAllContacts().Any())
                                ContactDisplay.DisplayContacts(ContactRepository.GetAllContacts());
                            else
                                Console.WriteLine("No contact found!");
                            break;
                        case 3:
                            string? search;
                            do
                            {
                                Console.Write("Enter the name to search: ");
                                search = Console.ReadLine();
                                if (search is not null)
                                    ContactDisplay.DisplayContacts(ContactSearch.SearchContactsByName(ContactRepository.GetAllContacts(), search));
                                else
                                    Console.WriteLine("No contacts found matching the search criteria.");

                            } while (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search));
                            break;
                        case 4:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid choice!");
                            break;
                    }
                }
            } while (choice != 4);
        }
    }
}
