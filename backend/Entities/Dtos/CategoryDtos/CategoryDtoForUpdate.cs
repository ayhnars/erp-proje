namespace Entities.Dtos.CategoryDtos
{
    // Mevcut kaydı güncelleme için DTO
    public class CategoryDtoForUpdate
    {
        public int CategoryID { get; set; }   // Güncelleme için ID gerekiyor
        public string CategoryType { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
