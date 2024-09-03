namespace JuniorHub.Domain.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }
    }
}
