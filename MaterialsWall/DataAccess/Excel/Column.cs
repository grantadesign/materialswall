namespace Granta.MaterialsWall.DataAccess.Excel
{
    public sealed class Column
    {
        public string Name{get {return name;}}
        private readonly string name;

        public int Index{get {return index;}}
        private readonly int index;

        public Column(string name, int index)
        {
            this.name = name;
            this.index = index;
        }
    }
}
