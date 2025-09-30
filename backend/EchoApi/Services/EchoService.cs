using EchoApi.Models;

namespace EchoApi.Services;

public interface IEchoService
{
    EchoResponse ProcessEcho(EchoRequest request);
}

public class EchoService : IEchoService
{
    private readonly ILogger<EchoService> _logger;

    public EchoService(ILogger<EchoService> logger)
    {
        _logger = logger;
    }

    public EchoResponse ProcessEcho(EchoRequest request)
    {
        _logger.LogInformation("Processing echo request with text: {Text}", request.Text);
        
        var response = new EchoResponse(request.Text);
        
        _logger.LogInformation("Echo response created successfully");
        
        return response;
    }
}