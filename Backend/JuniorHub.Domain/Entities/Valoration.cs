using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities
{
    public class Valoration
    {
        public int Id { get; set; }
        public ValorationEnum ValorationValue { get; set; }
        public string Reviewer {  get; set; }
        public int IdFreelancer { get; set; }
        public int IdEmployer { get; set; }
        public Freelancer Freelancer { get; set; }
        public Employer Employer { get; set; }
    }
}
