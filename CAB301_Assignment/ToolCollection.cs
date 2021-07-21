using System;

namespace CAB301_Assignment
{
    public class ToolCollection : iToolCollection
    {
        private int _Number;
        private Tool[] _Collection;

        public int Number
        {
            get { return _Number; }
        }

        public ToolCollection()
        {
            _Number = 0;
            _Collection = new Tool[30];
        }

        public void add(Tool aTool)
        {
            _Collection[_Number] = aTool;
            _Number++;
        }

        public void delete(Tool aTool)
        {
            int i = 0;
            while ((i < _Number) && (_Collection[i].CompareTo(aTool) != 0))
                i++;
            if (i == _Number)
                Console.WriteLine("Tool doesn't exist somehow");
            else
            {
                for (int j = i + 1; j < _Number; j++)
                    _Collection[j - 1] = _Collection[j];
                _Number--;
            }
        }

        public bool search(Tool aTool)
        {
            for (int i = 0; i < _Number; i++)
                if (_Collection[i].CompareTo(aTool) == 0)
                    return true;
            return false;
        }

        public Tool[] toArray()
        {
            return _Collection;
        }
    }
}
