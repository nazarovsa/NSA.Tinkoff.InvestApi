namespace NSA.Tinkoff.InvestApi.Options;

/// <summary>
/// Options of api client.
/// </summary>
public class InvestApiOptions
{
    /// <summary>
    /// Access token.
    /// </summary>
    public string AccessToken { get; set; }
    
    /// <summary>
    /// Name of an application.
    /// </summary>
    public string ApplicationName { get; set; }
}