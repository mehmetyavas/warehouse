namespace WebAPI.Data.Dto.ConfigDtos;

public class Mail
{
    public InvitationMail Register { get; set; } = null!;
    public InvitationMail InvitationRegistered { get; set; } = null!;
    public InvitationMail InvitationNotRegistered { get; set; } = null!;
}