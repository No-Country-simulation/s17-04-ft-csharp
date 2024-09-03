namespace JuniorHub.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int IdOffer { get; set; }
        public int IdFreelancer { get; set; }
        public bool Selected { get; set; }
        public Offer Offer { get; set; }
        public DateTime ApplicaionDate { get; set; }
        public Freelancer Freelancer { get; set; }
    }
}
