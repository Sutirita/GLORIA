using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCBM.Core.Console
{

        internal struct CommandInfo
        {
                private readonly IBaseCommand command;
                private readonly string name;
                private readonly string syntax;
                private readonly string help;
                private readonly string source;

                public CommandInfo(string CommandName, IBaseCommand command, string syntax, string help, string source)
                {
                        this.name = CommandName;
                        this.command = command;
                        this.syntax = (syntax ?? "");
                        this.help = (help ?? "");
                        this.source = source;
                }

                public IBaseCommand Command => command;
                public string GetName() => name;
                public string GetSyntax() => syntax;
                public string GetHelp() => help;
                public string GetSource() => source;
        }

        internal class CommandManager
        {
                

                private Dictionary<string, CommandInfo> commandDict;


                public void RegistCommand()
                {



                }






        }
}
