namespace Entities.Dtos
{

    public class ErpUserDtoforRegister : ErpUserDto
    {
        public string ConfirmPassword { get; init; }
        public string CompanyName { get; init; }
        public bool isBoss { get; init; }
        public string SignCode { get; init; }
    }


}