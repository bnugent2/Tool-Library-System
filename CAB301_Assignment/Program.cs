using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CAB301_Assignment
{
    class Program
    {
        private static Member LoggedIn;
        public static ToolLibrarySystem Library;
        public static MemberCollection Members; //Login side member collection

        // Get Option Methods
        static string[] GetToolTypes(int category)
        {
            string[] Gardening = { "Line Trimmers", "Lawn Mowers", "Hand Tools", "Wheelbarrows", "Garden Power Tools" };

            string[] Flooring = { "Scrapers", "Floor Lasers", "Floor Leveling Tools", "Floor Leveling Materials", "Floor Hand Tools", "Tiling Tools" };

            string[] Fencing = { "Hand Tools", "Electric Fencing", "Steel Fencing Tools", "Power Tools", "Fencing Accessories" };

            string[] Measuring = { "Distance Tools", "Laser Measurer", "Measuring Jugs", "Temperature & Humidity Tools", "Levelling Tools", "Markers" };

            string[] Cleaning = { "Draining", "Car Cleaning", "Vacuum", "Pressure Cleaner", "Pool Cleaning", "Floor Cleaning" };

            string[] Painting = { "Sanding Tools", "Brushes", "Rollers", "Paint Removal Tools", "Paint Scrapers", "Sprayers" };

            string[] Electronic = { "Voltage Testers", "Oscilloscopes", "Thermal Imaging", "Data Test Tool", "Insulation Testers" };

            string[] Electricity = { "Test Equipment", "Safety Equipment", "Basic Hand Tools", "Circuit Protection", "Cable Tools" };

            string[] Automotive = { "Jacks", "Air Compressors", "Battery Chargers", "Socket Tools", "Braking", "Drivetrain" };

            string[] Error = { "Error Occured" };

            switch (category)
            {
                case 1:
                    return Gardening;
                case 2:
                    return Flooring;
                case 3:
                    return Fencing;
                case 4:
                    return Measuring;
                case 5:
                    return Cleaning;
                case 6:
                    return Painting;
                case 7:
                    return Automotive;
                case 8:
                    return Electronic;
                case 9:
                    return Electricity;
            }
            return Error;

        }

        static public void GetCategoryType(out int category, out int type)
        {

            string[] ToolCategories = { "Gardening Tools", "Flooring Tools", "Fencing Tools", "Measuring Tools", "Cleaning Tools",
                                        "Painting Tools", "Automotive Tools", "Electronic Tools", "Electricity Tools" };
            for (int i = 0; i < ToolCategories.Length; i++)
            {
                Console.WriteLine("[{0}] {1}", i + 1, ToolCategories[i]);
            }
            Console.WriteLine("Select a Category (1-9):");
            int.TryParse(Console.ReadLine(), out category);
            string[] ToolType = GetToolTypes(category);
            for (int i = 0; i < ToolType.Length; i++)
            {
                Console.WriteLine("[{0}] {1}", i + 1, ToolType[i]);
            }
            Console.WriteLine("Select a Tool Type (1-6):");
            int.TryParse(Console.ReadLine(), out type);

            type -= 1;
            category -= 1;
        }


        ///////////////////////////
        /////// Staff Menus //////
        //////////////////////////

        /// <summary>
        /// Staff Login Method
        /// </summary>
        static void StaffLoginMenu()
        {
            string username, password;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Staff Login =====");
                Console.WriteLine("Enter Username:");
                username = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                password = Console.ReadLine();
                Console.WriteLine("Incorrect Username or Password - press any key to try again");

            } while (username != "staff" || password != "today123");

            StaffMenu();
        }

        /// <summary>
        /// GUI Method Display Staff Menu functions
        /// </summary>
        static void StaffMenu()
        {
            while (true)
            {
                int option;

                do
                {
                    Console.Clear();
                    Console.WriteLine("===== Staff Login Menu =====");

                    Console.WriteLine("[ 1 ] Add a new Tool");
                    Console.WriteLine("[ 2 ] Add Tool Quantity");
                    Console.WriteLine("[ 3 ] Remove Tool Quantity");
                    Console.WriteLine("[ 4 ] Register a new Member");
                    Console.WriteLine("[ 5 ] Remove a Member");
                    Console.WriteLine("[ 6 ] Find Member by Phone Number");
                    Console.WriteLine("[ 0 ] Return to main menu");

                } while (!int.TryParse(Console.ReadLine(), out option) || option < 0 || option > 6);

                Console.Clear();
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        Staff_AddToolMenu();
                        break;
                    case 2:
                        Staff_AddToolQuantity();
                        break;
                    case 3:
                        Staff_RemoveToolQuantity();
                        break;
                    case 4:
                        Staff_AddMember();
                        break;
                    case 5:
                        Staff_RemoveMember();
                        break;
                    case 6:
                        Staff_FindMemberPhone();
                        break;
                }
            }
        }

        /// <summary>
        /// GUI Method to Add Tool by staff
        /// </summary>
        static void Staff_AddToolMenu()
        {
            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Add New Tool =====");
            Console.WriteLine();
            Console.WriteLine(" Enter Tool Name:");
            string aToolName = Console.ReadLine();
            Tool aNewTool = new Tool(aToolName);
            Library.add(aNewTool);
            Console.WriteLine("Successfuly added {0} to the Library...Press Enter to Continue", aToolName);
            Console.ReadKey();
        }

        /// <summary>
        /// GUI Method to add member by staff
        /// </summary>
        static void Staff_AddMember()
        {
            Console.WriteLine(" ===== Tool Library - Add Member =====");
            Console.Write("Enter your First Name: ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter your Last Name: ");
            string Surname = Console.ReadLine();
            Console.Write("Enter your Contact Number: ");
            string Phone = Console.ReadLine();
            while (!Phone.All(char.IsDigit))
            {
                Console.WriteLine("Error: Phone mumber Must be valid- Please Try Again");
                Phone = Console.ReadLine();
            }
            Console.Write("Enter your Pin: ");
            string Pin = Console.ReadLine();
            while (!Pin.All(char.IsDigit))
            {
                Console.WriteLine("Error: The Pin must be a 4 digit number- Please Try Again");
                Pin = Console.ReadLine();
            }


            //ensure Member's names are capitalised
            FirstName = char.ToUpper(FirstName[0]) + FirstName.Substring(1);
            Surname = char.ToUpper(Surname[0]) + Surname.Substring(1);

            Member newMember = new Member(FirstName, Surname, Phone, Pin);
            Library.add(newMember); //add to library member collection
            Members.add(newMember); // also add to login validation member collection
            Console.WriteLine("Successfully added Member - {0} {1} to the Library....Pres Enter to Continue", FirstName, Surname);
            Console.ReadKey();

        }

        /// <summary>
        /// GUI Method to Remove Member by staff
        /// </summary>
        static void Staff_RemoveMember()
        {
            Console.WriteLine(" ===== Tool Library - Remove Member =====");
            int option;
            if (Members.Number > 0)
            {
                Console.WriteLine("List of Current Members:");
                Member[] people = Members.toArray();
                int numMembers = Members.Number;
                Console.WriteLine(people.ToStringTable(
        new[] { "Row #", "Name", "Phone Number" }, numMembers,
        a => Array.IndexOf(people, a) + 1, a => a.FirstName + " " + a.LastName, a => a.ContactNumber));
                Console.WriteLine("Select a Member to remove (Number of Row in Table):");
                int.TryParse(Console.ReadLine(), out option);
                Member MembertoDelete = people[option - 1];
                Members.delete(MembertoDelete); // Delete login side member
                Library.delete(MembertoDelete); // library side member
            }
            else
            {
                Console.WriteLine("No members currently Exist in the Library.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// GUI Method to Remove Tool Quantity by staff
        /// </summary>
        static void Staff_RemoveToolQuantity()
        {
            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Remove Quantity =====");
            Console.WriteLine();
            Tool dummy = new Tool("Dummy");
            Library.delete(dummy, 1);
        }

        /// <summary>
        /// GUI Method to Add Tool Quantity by staff
        /// </summary>
        static void Staff_AddToolQuantity()
        {
            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Add Quantity =====");
            Console.WriteLine();
            Tool dummy = new Tool("Dummy");
            Library.add(dummy, 1);

        }

        /// <summary>
        /// GUI Method to Find Member by Phone number
        /// </summary>
        static void Staff_FindMemberPhone()
        {
            bool found = false;
            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Find Member By Phone =====");
            Console.Write("Enter Member's Phone Number: ");
            string PhoneNum = Console.ReadLine();
            while (!PhoneNum.All(char.IsDigit))
            {
                Console.WriteLine("Error: Phone number must be all digits- Please Try Again");
                PhoneNum = Console.ReadLine();
            }
            Member[] member_arr = Members.toArray();

            foreach (Member person in member_arr)
            {
                if (person.ContactNumber == PhoneNum)
                {
                    Console.WriteLine("Member with Phone Number {0} is {1} {2}....Press Any Key to Continue", PhoneNum, person.FirstName, person.LastName);
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No Member exists in the library with Phone Number provided...Press Any Key to Continue");

            Console.ReadKey();
        }




        ///////////////////////////
        /////// Member Menus //////
        //////////////////////////

        /// <summary>
        /// Member Login functionality
        /// </summary>
        static void Member_LoginMenu()
        {
            string username, password;

            Console.Clear();
            Console.WriteLine("===== Member Login =====");
            Console.WriteLine("Enter Member Username:");
            username = Console.ReadLine();
            Console.WriteLine("Enter Pin:");
            password = Console.ReadLine();

            //Split username into first and last name for Member validation
            string[] names = Regex.Split(username, @"(?<!^)(?=[A-Z])");

            Member login = new Member(names[1], names[0], password);
            if (Members.search(login) == true)
            {
                LoggedIn = login;
                MemberMenu();
            }

            else
            {
                Console.WriteLine("Incorrect Username or Pin - press any key to try again");
                Console.ReadKey();

            }


        }

        /// <summary>
        /// Member functionality Main Menu
        /// </summary>
        static void MemberMenu()
        {
            while (true)
            {
                int option;

                do
                {
                    Console.Clear();
                    Console.WriteLine("\nMember Login Menu\n");

                    Console.WriteLine("[ 1 ] Display Tools by Category");
                    Console.WriteLine("[ 2 ] Borrow a Tool");
                    Console.WriteLine("[ 3 ] Return a Tool");
                    Console.WriteLine("[ 4 ] List My Loans");
                    Console.WriteLine("[ 5 ] Display Top 3 Borrowed Tools");
                    Console.WriteLine("[ 0 ] Return to main menu\n");

                } while (!int.TryParse(Console.ReadLine(), out option) || option < 0 || option > 5);

                Console.Clear();
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        Member_ToolsbyCat();
                        break;
                    case 2:
                        Member_Borrow();
                        break;
                    case 3:
                        Member_Return();
                        break;
                    case 4:
                        Member_ListBorrowed();
                        break;
                    case 5:
                        Member_TopThree();
                        break;
                }
            }
        }

        /// <summary>
        /// GUI Method to Display Tools By Category
        /// </summary>
        static void Member_ToolsbyCat()
        {

            Console.Clear();
            Console.WriteLine("===== List Tools By Category =====");
            int aToolType, aToolCategory;
            GetCategoryType(out aToolCategory, out aToolType);
            string query = aToolCategory.ToString() + "-" + aToolType.ToString();
            Library.displayTools(query);
            Console.ReadKey();

        }

        /// <summary>
        /// GUI Method for a Member to Borrow A tool
        /// </summary>
        static void Member_Borrow()
        {
            Console.Clear();
            Console.WriteLine("===== Borrow a Tool =====");
            Tool dummy = new Tool("Dummy");
            Library.borrowTool(LoggedIn, dummy);


        }

        /// <summary>
        /// Calls Tool Library borrow method
        /// </summary>
        static void Member_ListBorrowed()
        {
            Console.Clear();
            Console.WriteLine("===== Current Loans =====");
            Library.displayBorrowingTools(LoggedIn);

        }

        /// <summary>
        /// Calls Tool Library Top 3 method
        /// </summary>
        static void Member_TopThree()
        {
            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Top 3 =====");
            Library.displayTopThree();

        }


        /// <summary>
        /// Calls Tool Library Tool return method
        /// </summary>
        static void Member_Return()
        {

            Console.Clear();
            Console.WriteLine(" ===== Tool Library - Return Tool =====");
            Tool dummy = new Tool("Dummy");
            Library.returnTool(LoggedIn, dummy);
        }



        /// <summary>
        /// Whole Program Main Menu Select - transistions to either Staff or member menu
        /// </summary>
        static void MainMenu()
        {
            while (true)
            {
                int userChoice;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the Tool Library");
                    Console.WriteLine("========= Main Menu ============");
                    Console.WriteLine(" 1. Staff Login");
                    Console.WriteLine(" 2. Member Login");
                    Console.WriteLine(" 0. Exit");
                    Console.WriteLine("================================");
                    Console.WriteLine(" ");
                    Console.WriteLine("Please make a selection (1-2, or 0 to exit)");

                } while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > 2);

                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        StaffLoginMenu();
                        break;
                    case 2:
                        Member_LoginMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Not a valid option - Try again");
                        break;
                }
            }
        }


        static void Main(string[] args)
        {

            Library = new ToolLibrarySystem();
            Members = new MemberCollection();
            MainMenu();


        }

    }
}



