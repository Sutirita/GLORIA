using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GLORIA.API.Core.Console;

namespace GLORIA.Core.Console
{
        internal class CommandsCollection
        {
                public class Help : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Log("This is Help Command");
                                return CommandResult.Success;
                        }
                }




                public class TestCommand1 : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Log("This is Test1");
                                return CommandResult.Success;
                        }
                }
                public class TestCommand2 : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Log("This is Test2");
                                return CommandResult.Success;
                        }
                }






        }
}
