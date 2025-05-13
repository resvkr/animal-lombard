using System.ComponentModel.DataAnnotations;
using AnimalLombard.Utils;

namespace AnimalLombard.Modals;

public class User
{
    private static long _nextId = 1;
    [Key] public string Id { get; private set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 9)]
    public string PhoneNumber { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string PasswordHash { get; set; }

    [Required] public Role Role { get; set; }

    [Required] public bool IsActiveProfile { get; set; }

    private User(string name, string phoneNumber, string email, string hashedPassword)
    {
        Id = _nextId++.ToString();
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = hashedPassword;
        Role = Role.USER_ROLE;
        IsActiveProfile = true;
    }

    public override string ToString()
    {
        return $"User: {Name}, Phone: {PhoneNumber}, Email: {Email}, Role: {Role}, Active: {IsActiveProfile}";
    }

    public static User Create(string name, string phoneNumber, string email, string hashedPassword)
    {
        var user = new User(name, phoneNumber, email, hashedPassword);
        ValidatorUtils.ValidateEntity(user);
        return user;
    }
}