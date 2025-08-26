namespace Entities.Dtos.CategoryDtos
{
    // Normal DTO (listeleme / detay için)
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryType { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
