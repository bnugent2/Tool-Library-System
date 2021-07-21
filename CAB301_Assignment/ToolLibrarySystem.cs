using System;
using System.Collections.Generic;
namespace CAB301_Assignment
{
    public class ToolLibrarySystem : iToolLibrarySystem
    {
        //Current Logged in Member
        private MemberCollection Members;

        // Tool Category Collections

        private ToolCollection[][] _WholeCollection =
        {
        new ToolCollection[5],  //Gardening
        new ToolCollection[6] , //Flooring
        new ToolCollection[5] , //Fencing
        new ToolCollection[6] , //Measuring
        new ToolCollection[6] , //Cleaning
        new ToolCollection[6] , //Painting
        new ToolCollection[6], //Automotive
        new ToolCollection[5], //Electronic
        new ToolCollection[5]  //Electricity
            };


        public ToolLibrarySystem()
        {
            Members = new MemberCollection();

            //itterate through jagged array to initialise elements
            for (int n = 0; n < _WholeCollection.Length; n++)
            {
                for (int k = 0; k < _WholeCollection[n].Length; k++)
                {
                    _WholeCollection[n][k] = new ToolCollection();
                }
            }
        }

        /// <summary>
        /// Gets all Tool Sub Types for a particular Tool Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>String array of all Tool sub types</returns>
        private string[] GetToolTypes(int category)
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

        /// <summary>
        /// Gets user input for selecting tool type and category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="type"></param>
        private void GetCategoryType(out int category, out int type)
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

        /// <summary>
        /// If the given member object exists within the library system, return a reference to that member 
        /// </summary>
        /// <param name="aMember"></param>
        /// <returns> Member </returns>
        private Member FindMember(Member aMember)
        {
            Member[] member_arr = Members.toArray();
            foreach (Member person in member_arr)
            {
                if (person.CompareTo(aMember) == 0)
                    return person;
            }
            return null;
        }

        private Tool FindTool(Tool aTool)
        {
            Tool[] AllTools = GetAllTools();

            foreach (Tool _Tool in AllTools)
            {
                if (_Tool.CompareTo(aTool) == 0)
                    return _Tool;
            }

            return null;

        }

        /// <summary>
        /// Gets all tools within each category and type in the library
        /// </summary>
        /// <returns>An array of all tools within the library</returns>
        private Tool[] GetAllTools()
        {
            List<Tool> AllTools = new List<Tool>();

            for (int n = 0; n < _WholeCollection.Length; n++)
            {
                for (int k = 0; k < _WholeCollection[n].Length; k++)
                {
                    Tool[] ToolsinCollection = _WholeCollection[n][k].toArray();
                    int numTools = _WholeCollection[n][k].Number;

                    for (int i = 0; i < numTools; i++)
                    {
                        AllTools.Add(ToolsinCollection[i]);
                    }

                }
            }

            return AllTools.ToArray();
        }

        /// <summary>
        /// Adds a Tool go a given category and type Tool Collection
        /// </summary>
        /// <param name="aTool"></param>
        public void add(Tool aTool)
        {
            int aToolType, aToolCategory;
            GetCategoryType(out aToolCategory, out aToolType);
            _WholeCollection[aToolCategory][aToolType].add(aTool);

        }

        /// <summary>
        /// Increases the quantity of a selected tool
        /// </summary>
        /// <param name="aTool"></param>
        /// <param name="quantity"></param>
        public void add(Tool aTool, int quantity)
        {
            try
            {
                int aToolType, aToolCategory, option, qty;
                GetCategoryType(out aToolCategory, out aToolType);
                string param = aToolCategory.ToString() + "-" + aToolType.ToString();
                displayTools(param);
                Console.WriteLine("Select a Tool To Adjust:");
                int.TryParse(Console.ReadLine(), out option);
                Console.WriteLine("Increase Quantity by:");
                int.TryParse(Console.ReadLine(), out qty);
                Tool Tool = _WholeCollection[aToolCategory][aToolType].toArray()[option - 1];
                int finalqty = Tool.Quantity + qty;
                Tool.Quantity += qty;
                Tool.AvailableQuantity += qty;
                Console.WriteLine("Increased {0} to {1}..", Tool.Name, finalqty);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown Error Occured. Please Return to Main Menu and Try Again");
                return;
            }
        }

        /// <summary>
        /// Adds a member to the Tool Library System Member Collection
        /// </summary>
        /// <param name="aMember"></param>
        public void add(Member aMember)
        {
            Members.add(aMember);
        }

        /// <summary>
        /// Performs all the subclass operations needed for a member to borrow a tool
        /// </summary>
        /// <param name="aMember"></param>
        /// <param name="aTool"></param>
        public void borrowTool(Member aMember, Tool aTool)
        {
            int aToolType, aToolCategory, option;
            GetCategoryType(out aToolCategory, out aToolType);
            string param = aToolCategory.ToString() + "-" + aToolType.ToString();
            displayTools(param);
            Console.WriteLine("Select a Tool To Borrow:");
            int.TryParse(Console.ReadLine(), out option);
            Tool Tool = _WholeCollection[aToolCategory][aToolType].toArray()[option - 1];
            // Check to see if user is eligible to borrow this tool
            if (Tool.AvailableQuantity > 0)
            {
                Member borrower = FindMember(aMember);
                borrower.addTool(Tool);
                Tool.AvailableQuantity--;
                Tool.addBorrower(borrower);
                Console.WriteLine("Successfully Borrowed 1 {0}", Tool.Name);
                Console.ReadKey();
            }

        }

        /// <summary>
        /// Deletes a Tool from the ToolCollection
        /// </summary>
        /// <param name="aTool"></param>
        public void delete(Tool aTool)
        {
            int aToolType, aToolCategory, option, qty;
            GetCategoryType(out aToolCategory, out aToolType);
            string param = aToolCategory.ToString() + "-" + aToolType.ToString();
            displayTools(param);
            Console.WriteLine("Select a Tool To Delete:");
            int.TryParse(Console.ReadLine(), out option);
            _WholeCollection[aToolCategory][aToolType].delete(aTool);
        }

        /// <summary>
        /// Removes quantity of a given tool
        /// </summary>
        /// <param name="aTool"></param>
        /// <param name="quantity"></param>
        public void delete(Tool aTool, int quantity)
        {
            int aToolType, aToolCategory, option, qty;
            GetCategoryType(out aToolCategory, out aToolType);
            string param = aToolCategory.ToString() + "-" + aToolType.ToString();
            displayTools(param);
            Console.WriteLine("Select a Tool To Adjust:");
            int.TryParse(Console.ReadLine(), out option);
            Tool Tool = _WholeCollection[aToolCategory][aToolType].toArray()[option - 1];
            Console.WriteLine("Decrease Quantity by:");
            int.TryParse(Console.ReadLine(), out qty);
            int checkadjustment = Tool.Quantity -= qty;
            if (checkadjustment < 0)
            {
                Console.WriteLine("Cannot have Negative Quantity. Will set Tool Quantity to 0");
                Console.ReadKey();
            }
            else
            {
                Tool.Quantity -= qty;
                Tool.AvailableQuantity -= qty;
                Console.WriteLine("Decreased {0} to {1}..", Tool.Name, checkadjustment);
                Console.ReadKey();
            }


        }

        /// <summary>
        /// Deletes a member from the Tool Library Member Collection
        /// </summary>
        /// <param name="aMember"></param>
        public void delete(Member aMember)
        {
            Member MembertoDelete = FindMember(aMember);
            Members.delete(MembertoDelete);
            Console.WriteLine("Deleted Member {0} {1}..", MembertoDelete.FirstName, MembertoDelete.LastName);
            Console.ReadKey();
        }

        /// <summary>
        /// Writes to console a list of borrowed tools for a given member
        /// </summary>
        /// <param name="aMember"></param>
        public void displayBorrowingTools(Member aMember)
        {
            Member user = FindMember(aMember);

            Console.Clear();
            Console.WriteLine(user.Tools.ToStringTable(
       new[] { "Row #", "Tool Name" }, user.Tools.Length,
        a => Array.IndexOf(user.Tools, a) + 1, a => a));
            Console.ReadKey();
        }

        /// <summary>
        /// Displays the list of tools in a given Tool category and Sub Type
        /// </summary>
        /// <param name="aToolType"></param>
        public void displayTools(string aToolType)
        {
            int category, type;
            string[] indexes = aToolType.Split('-');

            int.TryParse(indexes[0], out category);
            int.TryParse(indexes[1], out type);
            Tool[] ToolTypeTools = _WholeCollection[category][type].toArray();
            int numTools = _WholeCollection[category][type].Number;
            if (numTools == 0)
            {
                Console.WriteLine("No Tools Currently Exist for that Tool Type. Press Enter To Return to Main Menu");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine(ToolTypeTools.ToStringTable(
       new[] { "Row #", "Tool Name", "Total Quantity", "Available Quantity" }, numTools,
        a => Array.IndexOf(ToolTypeTools, a) + 1, a => a.Name, a => a.Quantity, a => a.AvailableQuantity));
            Console.ReadKey();
        }

        /// <summary>
        /// Writes to console the top 3 borrowed tools for the library
        /// </summary>
        public void displayTopThree()
        {
            Tool[] AllTools = GetAllTools();

            if (AllTools.Length < 3)
            {
                Console.WriteLine("Need to have at least 3 Tools in the library to do this function");
                return;
            }


            QuickSort.Sort(AllTools, 0, AllTools.Length - 1);

            Console.WriteLine(AllTools.ToStringTable(
             new[] { "Row #", "Tool Name", "# Borrowings" }, 3,
             a => Array.IndexOf(AllTools, a) + 1, a => a.Name, a => a.NoBorrowings));
            Console.ReadKey();
        }

        /// <summary>
        /// Returns a string array of a members borrowed tools
        /// </summary>
        /// <param name="aMember"></param>
        /// <returns></returns>
        public string[] listTools(Member aMember)
        {
            Member member = FindMember(aMember);
            return member.Tools;
        }
        /// <summary>
        /// Performs all the subclass operations needed for a member to return a tool
        /// </summary>
        /// <param name="aMember"></param>
        /// <param name="aTool"></param>
        public void returnTool(Member aMember, Tool aTool)
        {
            int option;
            Member Mem_Return = FindMember(aMember);
            displayBorrowingTools(Mem_Return);
            Console.WriteLine("Select a Tool to return:");
            int.TryParse(Console.ReadLine(), out option);
            string[] MemberLoans = listTools(Mem_Return);
            Tool selected = new Tool(MemberLoans[option - 1]);

            //Deal with tool object
            Tool returning = FindTool(selected);
            returning.AvailableQuantity++;
            MemberCollection borrowers = returning.GetBorrowers();
            borrowers.delete(Mem_Return);
            //Deal with member object
            Mem_Return.deleteTool(returning);
            Console.WriteLine("Returned Tool {0}..", returning.Name);
            Console.ReadKey();


        }
    }
}
