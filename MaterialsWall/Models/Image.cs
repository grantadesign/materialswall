namespace Granta.MaterialsWall.Models
{
    public sealed class Image
    {
        public int Index{get {return index;}}
        private readonly int index;

        public Image(int index)
        {
            this.index = index;
        }
    }
}
