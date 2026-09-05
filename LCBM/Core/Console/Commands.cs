using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCBM.Core.Console
{
        internal class Commands
        {
                public class Help : IBaseCommand
                {
                        CommandResult IBaseCommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Instance.Log("This is Help Command");
                                return CommandResult.Success;
                        }
                }




                public class TestCommand1 : IBaseCommand
                {
                        CommandResult IBaseCommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Instance.Log("This is Test1");
                                return CommandResult.Success;
                        }
                }
                public class TestCommand2 : IBaseCommand
                {
                        CommandResult IBaseCommand.Execute(string[] args, CommandContext context)
                        {
                                Console.Instance.Log("This is Test2");
                                return CommandResult.Success;
                        }
                }






        }
}
