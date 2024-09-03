using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities
{
    public class Freelancer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ValorationEnum Valoration { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public ICollection<Technology> Technologies { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Link> Links { get; set; }
        public ICollection<Application> Applications { get; set; }
        //Coleccion por que maximo va a tener 2, donde valora y donde es valorado.
        public ICollection<Valoration> Valorations { get; set; }
    }
}
