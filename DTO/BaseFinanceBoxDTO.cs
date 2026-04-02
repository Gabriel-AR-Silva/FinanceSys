using System.Reflection.Metadata;
using System;

public class BaseFinanceBoxDTO
{
    public required string Name { get; set; }
    

    public virtual List<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add("Name is required");
            return errors;
        }

        if (Name.Length < 2 || Name.Length > 150)
        {
            errors.Add($"The name requires between 2 and 150 characters. (Length: {Name.Length})");
        }

        return errors;
    }
}