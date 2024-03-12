using Command.Main;
using System.Collections.Generic;

namespace Command.Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();

        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegisterCommand(commandToProcess);
        }

        public void ExecuteCommand(ICommand commandToExecute) => commandToExecute.Execute();

        public void RegisterCommand(ICommand commandToRegister) => commandRegistry.Push(commandToRegister);
        private bool RegisterEmpty() => commandRegistry.Count == 0;
        private bool CommandBelongsToActivePlayer() => (commandRegistry.Peek() as UnitCommand).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID;
        public void Undo()
        {
            if (!RegisterEmpty() && CommandBelongsToActivePlayer())
                commandRegistry.Pop().Undo();
        }
    }
}