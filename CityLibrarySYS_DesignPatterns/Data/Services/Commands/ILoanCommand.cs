using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services.Commands
{
    // The Command interface: declares a method for executing an operation.
    public interface ILoanCommand
    {
        Task Execute();
    }
}