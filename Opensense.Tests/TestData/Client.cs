namespace OpenSense.Tests.TestData;

internal class Client
{
    public string Name { get; set; } = string.Empty;
    public Contract? Contract { get; set; }
    public List<Member> Members { get; set; } = [];
}
