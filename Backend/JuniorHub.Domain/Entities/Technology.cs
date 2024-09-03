namespace JuniorHub.Domain.Entities
{
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Freelancer> Freelancers { get; set; }

    }
}
