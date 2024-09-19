namespace JuniorHub.Application.Contracts.SendGrid;

public interface IEmailService
{
    Task<bool> SendEmailToFreelancer(int freelancerId, int offerId);
}
