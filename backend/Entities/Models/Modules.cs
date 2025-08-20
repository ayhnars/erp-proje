namespace Entities
{
    public class Modules
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public string Icon { get; set; }
        public string Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}