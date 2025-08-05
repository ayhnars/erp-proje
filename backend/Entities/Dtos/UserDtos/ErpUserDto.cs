namespace Entities.Dtos
{ 
    public class ErpUserDto
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string role { get; set; }
        public string Tags { get; set; }
        public string createdAt { get; set; }
    }
}