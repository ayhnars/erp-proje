namespace Entities.Dtos
{

    public class ErpUserDtoforRegister
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public bool isBoss { get; set; }
        public string SignCode { get; set; }
    }


}