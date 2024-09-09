namespace JuniorHub.Domain.Entities;

public class Application
{
    public int Id { get; set; }
    public int? OfferId { get; set; }
    public Offer Offer { get; set; }
    public int? FreelancerId { get; set; } 
    public Freelancer Freelancer { get; set; }
    public bool Selected { get; set; }
    public DateTime ApplicaionDate { get; set; }
}
