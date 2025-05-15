namespace Contacts.Entities;

public class Contact{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Email{get; set;} = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
}