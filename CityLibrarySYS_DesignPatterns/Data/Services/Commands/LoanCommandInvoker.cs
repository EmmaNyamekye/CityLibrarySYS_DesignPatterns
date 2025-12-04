using System;
using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services.Commands
{
    // The Invoker: holds a command and knows when to execute it.
    public class LoanCommandInvoker
    {
        private ILoanCommand? _command;

        // Sets the command to be executed
        public void SetCommand(ILoanCommand command)
        {
            _command = command;
        }

        // Triggers the command execution
        public async Task InvokeCommand()
        {
            if (_command == null)
            {
                throw new InvalidOperationException("No command has been set.");
            }

            await _command.Execute();
        }
    }
}