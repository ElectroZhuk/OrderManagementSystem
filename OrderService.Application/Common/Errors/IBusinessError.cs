namespace OrderService.Application.Common.Errors;

public interface IBusinessError
{
    string Code { get; }
    string Description { get; }
}