using CatalogService.Application.Common.Logging;
using Serilog;

namespace CatalogService.Infrastructure.Common.Logging;

public class SerilogLogger(ILogger logger) : IAppLogger
{
    public void Information(string messageTemplate, params object[] propertyValues)
    {
        logger.Information(messageTemplate, propertyValues);
    }

    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        logger.Debug(messageTemplate, propertyValues);
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        logger.Warning(messageTemplate, propertyValues);
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        logger.Error(messageTemplate, propertyValues);
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        logger.Error(exception, messageTemplate, propertyValues);
    }
}