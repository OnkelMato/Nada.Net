namespace Nada.Replacer;

/// <summary>
///     The Property library is designed to replace parameters in string.
///     Patterns are used to replace integers, doubles, dates, and not string-based values.
///     The purpose is a flexible library to replace key-value from a string->string dictionary.
///     <para>Token structure</para>
///     Token is surrounded by { }. Each token consists of 2 or 3 parts separated by |.
///     Token sample: {dataType|tokenName|additionalInformation}
///     <para>Data type</para>
///     First part of the token represents data type of the replacement string. Data type is not case sensitive. Allowed
///     data types are:
///     <list type="bullet">
///         <item>string</item>
///         <item>datetime</item>
///         <item>dateonly</item>
///         <item>timeonly</item>
///         <item>guid</item>
///         <item>link</item>
///         <item>plurality</item>
///     </list>
///     <para>Token name</para>
///     The second part of the token represents token name.
///     This token name is the key if the dictionary.
///     It is not case sensitive.Allowed characters are a-z, A-Z, - and _.
///     <para>Additional information</para>
///     The last part is optional for some data types.
///     Please see wiki for examples.
/// </summary>
public interface IPropertyParser
{
    string Parse(string template, IDictionary<string, string>? @params);
}