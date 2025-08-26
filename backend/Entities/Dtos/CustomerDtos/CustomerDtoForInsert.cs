namespace Entities.Dtos.CustomerDtos
{
    public class CustomerDtoForInsert
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CompanyID { get; set; }
    }
}
