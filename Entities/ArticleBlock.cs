namespace upp.Entities
{
    public class ArticleBlock
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }
    }
}
