namespace Nada.Replacer.Handlers;

public interface ITokenTypeHandler
{
    public bool CanHandle(string dataType);

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params);
}