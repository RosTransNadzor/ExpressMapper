using System.Text;

namespace ExpressMapperCore;

public class NameHelper
{
    public static string ToUpperFirstLatter(string name)
    {
        StringBuilder builder = new StringBuilder(name.Length);
        builder.Append(char.ToUpper(name[0]));
        builder.Append(name.AsSpan(1));
        return builder.ToString();
    }
}