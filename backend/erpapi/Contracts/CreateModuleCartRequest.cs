namespace erpapi.Contracts;

public record CreateModuleCartRequest(
    string UserId,
    int CompanyID,
    decimal TotalPrice,
    string? Status
);