using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime EstimatedTime { get; set; }
        public State State { get; set; }
        public Difficult Difficult { get; set; }
        public int IdEmployer { get; set; }
        public Employer Employer { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
