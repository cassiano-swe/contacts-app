namespace Contacts.Entities;

public class Contact{
    public Guid Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string CountryCode{get; set;} = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty;
}