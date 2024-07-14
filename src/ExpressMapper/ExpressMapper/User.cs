using System.Text;

namespace ExpressMapper;

public class User
{
    //auto mapping
    public string UserName { get; init; }
    public string Description { get; init; }
    // ignore by source
    public string Password { get; init; }
    // ignore by dest
    public bool IsActive { get; init; }
    // map to member
    public Guid ID { get; init; }
    //for constructor
    public DateTime WasBorn { get; init; }
    //not mapped
    public int Age { get; set; }
    //ignore because incorrect type 
    public string Gender { get; set; }
    
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("User info:");
        // автоматически
        builder.AppendLine($"\tUserName: {UserName}");
        //игнорируется в маппере
        builder.AppendLine($"\tPassword: {Password}");
        //мапится из поля GlobalId у UserDto
        builder.AppendLine($"\tID {ID}");
        return builder.ToString();
    }
}