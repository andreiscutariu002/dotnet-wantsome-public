using System;

public class Service : IService
{
    public string SimpleMethod()
    {
        return string.Format("Demo");
    }

    public string GetData(int value)
    {
        return string.Format("You entered: {0}", value);
    }

    public CompositeType GetDataUsingDataContract(CompositeType composite)
    {
        if (composite == null)
        {
            throw new ArgumentNullException("composite");
        }

        if (composite.BoolValue)
        {
            composite.StringValue += "Suffix";
        }

        return composite;
    }
}
