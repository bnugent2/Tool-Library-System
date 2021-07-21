namespace CAB301_Assignment
{
    /// <summary>
    /// Abstracts away BST Class Methods - underlying datastructure is BST
    /// </summary>
    public class MemberCollection : iMemberCollection
    {
        private BSTree MembersBST = new BSTree();
        private int _Number;
        public int Number { get { return _Number; } }

        public void add(Member aMember)
        {
            MembersBST.Insert(aMember);
            _Number++;
        }

        public void delete(Member aMember)
        {
            MembersBST.Delete(aMember);
            _Number--;
        }

        public bool search(Member aMember)
        {
            return MembersBST.Search(aMember);
        }

        public Member[] toArray()
        {
            return MembersBST.InOrderTraverse();
        }
    }
}
