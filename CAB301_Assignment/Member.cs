using System;
using System.Linq;

namespace CAB301_Assignment
{
    public class Member : iMember, IComparable<Member>
    {

        private string _FirstName;
        private string _LastName;
        private string _ContactNumber;
        private string _PIN;
        private int _noTools;
        private string[] _Tools = new string[1];

        // Normal constructor
        public Member(string aFirstName, string aLastName, string aContactNumber, string aPIN)
        {
            _FirstName = aFirstName;
            _LastName = aLastName;
            _ContactNumber = aContactNumber;
            _PIN = aPIN;
            _noTools = 0;
        }

        //Constructor to use within Tool Borrowers Collection
        public Member(string aFirstName, string aLastName)
        {
            _FirstName = aFirstName;
            _LastName = aLastName;
            _noTools = 0;
        }

        //Constructor to use for login
        public Member(string aFirstName, string aLastName, string aPIN)
        {
            _FirstName = aFirstName;
            _LastName = aLastName;
            _PIN = aPIN;
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        public string PIN
        {
            get { return _PIN; }
            set { _PIN = value; }
        }

        public string[] Tools
        {
            get { return _Tools; }
        }


        public void addTool(Tool aTool)
        {
            if ((_Tools.Length >= 1) && (_Tools.Length < 3))
            {
                Array.Resize(ref _Tools, _noTools + 1);
            }
            if (_noTools < 3)
            {
                _Tools[_noTools] = aTool.Name;
                _noTools++;

            }
            else
            {
                Console.WriteLine("Member Cannot Borrow more than 3 Tools at Once");
            }
        }

        public void deleteTool(Tool aTool)
        {
            _Tools = _Tools.Where(o => o != aTool.Name).ToArray();
            _noTools--;
        }

        public int CompareTo(Member other)
        {
            if (_LastName.CompareTo(other.LastName) < 0)
                return -1;
            else
                if ((_LastName.CompareTo(other.LastName) == 0) && (_FirstName.CompareTo(other.FirstName) == 0) && (_PIN.CompareTo(other.PIN) == 0))
                return 0;
            else
                return 1;
        }

        public override string ToString()
        {
            return (_FirstName + " " + _LastName + "\n");
        }
    }
}
