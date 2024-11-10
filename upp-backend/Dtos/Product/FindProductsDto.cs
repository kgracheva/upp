namespace upp.Dtos.Product
{
    public class FindProductsDto
    {
        public string SearchName { get; set; } = string.Empty;
        public int CreatorId { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
