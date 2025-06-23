namespace OrderService.Application.Logging;

public interface IAppLogger
{
    void Information(string messageTemplate, params object[] propertyValues);
    
    void Debug(string messageTemplate, params object[] propertyValues);
    
    void Warning(string messageTemplate, params object[] propertyValues);
    
    void Error(string messageTemplate, params object[] propertyValues);

    void Error(Exception exception, string messageTemplate, params object[] propertyValues);
}