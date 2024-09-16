using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.SendGrid;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.SendGrid.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.SendGrid.Services
{
    internal class SendGridEmailService : IEmailService
    {
        private readonly SendGridConfiguration _sendGridConfiguration;
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IEmployerRepository _employerRepository;
        public SendGridEmailService(SendGridConfiguration sendGridConfiguration, IFreelancerRepository freelancerRepository, IOfferRepository offerRepository, IEmployerRepository employerRepository)
        {
            _sendGridConfiguration = sendGridConfiguration;
            _freelancerRepository = freelancerRepository;
            _offerRepository = offerRepository;
            _employerRepository = employerRepository;
        }

        public async Task<bool> SendEmailToFreelancer(int freelancerId, int offerId, int employerId)
        {
            var freelancerWithOutInclude = await _freelancerRepository.GetByIdAsync(freelancerId);
            var freelancer = await _freelancerRepository.GetProfileFreelancer(freelancerWithOutInclude.UserId);
            var offer = await _offerRepository.GetFullOfferAsync(offerId);
            var employer = await _employerRepository.GetProfileEmployer(employerId);

            SendGridClient client = new SendGridClient(_sendGridConfiguration.ApiKey);

            // Configuración del email de origen
            EmailAddress fromEmailAddress = new EmailAddress("s17-04-ft-csharp@proton.me", "JuniorHub");

            // Configuración del email del destinatario (freelancer)
            EmailAddress toEmailAddres = new EmailAddress(freelancer.User.Email, string.Concat(freelancer.User.Name, " ", freelancer.User.LastName));

            // Asunto personalizado
            string subject = $"🎉 ¡Felicidades {freelancer.User.Name}! ¡Has sido seleccionado para una oferta!";

            // Contenido en texto plano
            string plainTextContent = $@"
                Hola {freelancer.User.Name} {freelancer.User.LastName},

                ¡Estamos emocionados de informarte que has sido seleccionado para la siguiente oferta!

                Título de la oferta: {offer.Title}
                Descripción: {offer.Description}
                Precio: ${offer.Price}
                Empleador: {employer.User.Name} {employer.User.LastName}

                Por favor, responde a este correo para contactar con {employer.User.Name} y acordar los siguientes pasos.

                ¡Felicitaciones nuevamente!
                Saludos cordiales,
                El equipo de JuniorHub";

            string htmlContent = $@"
                <html>
                <body>
                    <h2 style='color: #4CAF50;'>🎉 ¡Felicidades {freelancer.User.Name}!</h2>
                    <p>
                        Nos complace informarte que has sido seleccionado para una oferta en <strong>JuniorHub</strong>.
                        Aquí tienes los detalles de la oferta:
                    </p>
                    <ul>
                        <li><strong>Título de la oferta:</strong> {offer.Title}</li>
                        <li><strong>Descripción:</strong> {offer.Description}</li>
                        <li><strong>Precio:</strong> ${offer.Price}</li>
                        <li><strong>Empleador:</strong> {employer.User.Name} {employer.User.LastName}</li>
                    </ul>
                    <p>
                        Por favor, <strong>responde a este correo</strong> para contactar directamente con {employer.User.Name} y acordar los próximos pasos.
                    </p>
                    <br />
                    <p>Saludos cordiales,<br/>El equipo de JuniorHub</p>
                </body>
                </html>";

            var notificationMessage = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddres, subject, plainTextContent, htmlContent);

            notificationMessage.ReplyTo = new EmailAddress(employer.User.Email, string.Concat(employer.User.Name, " ", employer.User.LastName));

            var sendEmailResponse = await client.SendEmailAsync(notificationMessage);

            return sendEmailResponse.IsSuccessStatusCode;
        }
    }
}
