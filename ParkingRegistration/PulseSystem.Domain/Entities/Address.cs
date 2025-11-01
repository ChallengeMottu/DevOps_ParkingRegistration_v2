namespace PulseSystem.Domain.Entities;

public class Address
{
    public string? Street { get; set; } = string.Empty;
    public string Complement { get; set; }= string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    
    public void ValidaCep(string cep)
    {
        if (string.IsNullOrEmpty(cep) || cep.Length != 8) throw new ArgumentException("CEP inválido");
    }
}