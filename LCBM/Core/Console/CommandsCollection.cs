using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LCBM.API.Core.Console;

namespace LCBM.Core.Console
{
        internal class CommandsCollection
        {
                public class Help : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                LCBMConsole.Log("This is Help Command");
                                return CommandResult.Success;
                        }
                }




                public class TestCommand1 : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                LCBMConsole.Log("This is Test1");
                                return CommandResult.Success;
                        }
                }
                public class TestCommand2 : ICommand
                {
                        CommandResult ICommand.Execute(string[] args, CommandContext context)
                        {
                                LCBMConsole.Log("This is Test2");
                                return CommandResult.Success;
                        }
                }






        }
}
