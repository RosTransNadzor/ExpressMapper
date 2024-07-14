using System.Text;

namespace ExpressMapper;

public class UserDto
{
    //auto mapping
    public string UserName { get; init; }
    public string Description { get; init; }
    //ignore by source
    public string Password { get; init; }
    //ignore by dest
    public bool IsActive { get; set; }
    // map to value 
    public string Email { get; init; }
    //map to member
    public Guid GlobalID { get; init; }
    // constructor params first letter to Upper
    public DateTime ImportantDay { get; }

    public UserDto(DateTime wasBorn)
    {
        ImportantDay = wasBorn;
    }
    // not mapped because cannot be written
    public int Age { get; }
    //ignored because incorrect source type
    public int Gender { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Dto info:");
        builder.AppendLine($"\tUserName: {UserName}");
        builder.AppendLine($"\tDescription: {Description}");
        builder.AppendLine($"\tPassword: {Password ?? "password is null"}");
        builder.AppendLine($"\tIsActive: {IsActive}");
        builder.AppendLine($"\tEmail: {Email}");
        builder.AppendLine($"\tGlobalID: {GlobalID}");
        builder.AppendLine($"\tAge: {Age}");
        builder.AppendLine($"\tGender: {Gender}");
        return builder.ToString();
    }
}