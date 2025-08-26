namespace Entities.Dtos.CategoryDtos
{
    // Yeni kayıt ekleme için DTO
    public class CategoryDtoForInsert
    {
        public string CategoryType { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
