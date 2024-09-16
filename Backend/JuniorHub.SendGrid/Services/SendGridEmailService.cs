using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.SendGrid;
using JuniorHub.SendGrid.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JuniorHub.SendGrid.Services;

internal class SendGridEmailService : IEmailService
{
    private readonly SendGridConfiguration _sendGridConfiguration;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly IOfferRepository _offerRepository;

    public SendGridEmailService(
        SendGridConfiguration sendGridConfiguration,
        IFreelancerRepository freelancerRepository,
        IOfferRepository offerRepository 
        )
    {
        _sendGridConfiguration = sendGridConfiguration;
        _freelancerRepository = freelancerRepository;
        _offerRepository = offerRepository;
    }

    public async Task<bool> SendEmailToFreelancer(int freelancerId, int offerId)
    {
        var offer = await _offerRepository.GetFullOfferAsync(offerId);

        var userFreelancer = await _freelancerRepository.GetFreelancerUser(freelancerId);

        SendGridClient client = new SendGridClient(_sendGridConfiguration.ApiKey);

        // Configuración del email de origen
        EmailAddress fromEmailAddress = new EmailAddress("s17-04-ft-csharp@proton.me", "JuniorHub");

        // Configuración del email del destinatario (freelancer)
        EmailAddress toEmailAddres = new EmailAddress(userFreelancer.Email, string.Concat(userFreelancer.Name, " ", userFreelancer.LastName));

        // Asunto personalizado
        string subject = $"🎉 ¡Felicidades {userFreelancer.Name}! ¡Has sido seleccionado para una oferta!";

        // Contenido en texto plano
        string plainTextContent = $@"
                Hola {userFreelancer.Name} {userFreelancer.LastName},

                ¡Estamos emocionados de informarte que has sido seleccionado para la siguiente oferta!

                Título de la oferta: {offer.Title}
                Descripción: {offer.Description}
                Precio: ${offer.Price}
                Empleador: {offer.Employer.User.Name} {offer.Employer.User.LastName}

                Por favor, responde a este correo para contactar con {offer.Employer.User.Name} y acordar los siguientes pasos.

                ¡Felicitaciones nuevamente!
                Saludos cordiales,
                El equipo de JuniorHub";

        string htmlContent = $@"
                <html>
                <body>
                    <h2 style='color: #4CAF50;'>🎉 ¡Felicidades {userFreelancer.Name}!</h2>
                    <p>
                        Nos complace informarte que has sido seleccionado para una oferta en <strong>JuniorHub</strong>.
                        Aquí tienes los detalles de la oferta:
                    </p>
                    <ul>
                        <li><strong>Título de la oferta:</strong> {offer.Title}</li>
                        <li><strong>Descripción:</strong> {offer.Description}</li>
                        <li><strong>Precio:</strong> ${offer.Price}</li>
                        <li><strong>Empleador:</strong> {offer.Employer.User.Name} {offer.Employer.User.LastName}</li>
                    </ul>
                    <p>
                        Por favor, <strong>responde a este correo</strong> para contactar directamente con {offer.Employer.User.Name} y acordar los próximos pasos.
                    </p>
                    <br />
                    <p>Saludos cordiales,<br/>El equipo de JuniorHub</p>
                </body>
                </html>";

        var notificationMessage = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddres, subject, plainTextContent, htmlContent);

        notificationMessage.ReplyTo = new EmailAddress(offer.Employer.User.Email, string.Concat(offer.Employer.User.Name, " ", offer.Employer.User.LastName));

        var sendEmailResponse = await client.SendEmailAsync(notificationMessage);

        return sendEmailResponse.IsSuccessStatusCode;
    }
}
