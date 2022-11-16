using PropertyManagement.Application.Models;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Contracts.Infastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
