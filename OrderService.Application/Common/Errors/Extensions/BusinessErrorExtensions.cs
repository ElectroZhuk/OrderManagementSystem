using ErrorOr;

namespace OrderService.Application.Common.Errors.Extensions;

public static class BusinessErrorExtensions
{
    public static Error AsNotFound(this IBusinessError businessErrorInfo, Dictionary<string, object?>? metadata = null)
    {
        return Error.NotFound(businessErrorInfo.Code, businessErrorInfo.Description, metadata);
    }
    
    public static Error AsConflict(this IBusinessError businessErrorInfo, Dictionary<string, object?>? metadata = null)
    {
        return Error.Conflict(businessErrorInfo.Code, businessErrorInfo.Description, metadata);
    }
    
    public static Error AsValidation(this IBusinessError businessErrorInfo, Dictionary<string, object?>? metadata = null)
    {
        return Error.Validation(businessErrorInfo.Code, businessErrorInfo.Description, metadata);
    }
    
    public static Error AsFailure(this IBusinessError businessErrorInfo, Dictionary<string, object?>? metadata = null)
    {
        return Error.Failure(businessErrorInfo.Code, businessErrorInfo.Description, metadata);
    }
    
    public static Error AsUnexpected(this IBusinessError businessErrorInfo, Dictionary<string, object?>? metadata = null)
    {
        return Error.Unexpected(businessErrorInfo.Code, businessErrorInfo.Description, metadata);
    }
}