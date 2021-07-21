using System;

namespace CAB301_Assignment
{
    public class Tool : iTool, IComparable<Tool>
    {
        private string _Name;
        private int _Quantity;
        private int _AvailableQuantity;
        private int _NoBorrowings;
        private MemberCollection _borrowers;
        public string Name
        {
            get { return _Name; }   // get method
            set { _Name = value; }  // set method 
        }
        public int Quantity
        {
            get { return _Quantity; }   // get method
            set { _Quantity = value; }  // set method 
        }
        public int AvailableQuantity
        {
            get { return _AvailableQuantity; }   // get method
            set { _AvailableQuantity = value; }  // set method 
        }
        public int NoBorrowings
        {
            get { return _NoBorrowings; }   // get method
            set { _NoBorrowings = value; }  // set method 
        }

        // Not sure why but it says I'm not implimenting interface but I have the method further down???
        MemberCollection iTool.GetBorrowers => throw new NotImplementedException();

        //Find tool Constructor
        public Tool(string aName)
        {
            _Name = aName;
            _NoBorrowings = 0;
            _AvailableQuantity = 1;
            _Quantity = 1;
            _borrowers = new MemberCollection();
        }

        //Class Methods
        public MemberCollection GetBorrowers()
        {

            return _borrowers;
        }

        public void addBorrower(Member aMember)
        {
            _borrowers.add(aMember);
            _NoBorrowings++;
        }

        public void deleteBorrower(Member aMember)
        {
            _borrowers.delete(aMember);
            _NoBorrowings--;
        }

        public override string ToString()
        {
            return (String.Format("{0} {1} {2} ", _Name, _AvailableQuantity, _Quantity));
        }

        public int CompareTo(Tool other)
        {
            if (_Name.CompareTo(other.Name) == 0)
            {
                return 0;
            }
            else return -1;
        }
    }
}
