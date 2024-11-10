namespace upp.Entities
{
    public class TrainingBlock
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }
    }
}
