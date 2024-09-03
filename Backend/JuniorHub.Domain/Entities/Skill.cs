namespace JuniorHub.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Freelancer> Freelancers { get; set; } = null!;
    }
}
