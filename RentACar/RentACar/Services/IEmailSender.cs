using RentACar.Models;

namespace RentACar.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
