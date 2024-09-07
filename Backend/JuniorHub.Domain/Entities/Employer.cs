using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class Employer
{
    public int Id { get; set; }
    public ValorationEnum Valoration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Offer> Offers { get; set; }
    //Coleccion por que maximo va a tener 2, donde valora y donde es valorado.
    public ICollection<Valoration> Valorations { get; set; }
}
