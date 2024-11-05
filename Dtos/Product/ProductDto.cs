namespace upp.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ProteinsCount { get; set; }
        public int FatsCount { get; set; }
        public int CarbsCount { get; set; }
        public int CaloriesCount { get; set; }
        public int CreatorId { get; set; }
    }
}
