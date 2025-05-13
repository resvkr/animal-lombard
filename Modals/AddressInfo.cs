using System.ComponentModel.DataAnnotations;
using AnimalLombard.Utils;

namespace AnimalLombard.Modals;

public class AddressInfo
{
    [Required(ErrorMessage = "Please enter the address.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Please enter the city.")]
    public string City { get; set; }

    private AddressInfo(string address, string city)
    {
        Address = address;
        City = city;
    }

    public override string ToString()
    {
        return $"AddressInfo: Address: {Address}, City: {City}";
    }

    public static AddressInfo Create(string address, string city)
    {
        var addressInfo = new AddressInfo(address, city);
        ValidatorUtils.ValidateEntity(addressInfo);
        return addressInfo;
    }
}