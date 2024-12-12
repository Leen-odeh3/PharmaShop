using Microsoft.Extensions.Logging;

namespace pharmacy.Core.ILogger;
public interface ILog
{
    public void Log(string message, string Type);
}
