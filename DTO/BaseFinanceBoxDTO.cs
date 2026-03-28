using System.Reflection.Metadata;
using System;

public class BaseFinanceBoxDTO
{
    public required string Name { get; set; }

    public virtual List<string> Validate(string name)
    {
        var errors = new List<string>;

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add();
        }
    }
}