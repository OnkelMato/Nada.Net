namespace Nada.Replacer.Handlers;

public record TokenHandlerResult(string Value, bool IsSuccess = true)
{
    public string Value { get; set; } = Value;

    public bool IsSuccess { get; set; } = IsSuccess;
}