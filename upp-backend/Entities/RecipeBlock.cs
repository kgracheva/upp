namespace upp.Entities
{
    public class RecipeBlock
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }
    }
}
