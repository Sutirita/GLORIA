using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LCBM.Core.Console.IBaseCommand;

namespace LCBM.Core.Console
{
        public interface IBaseCommand
        {
                /// <summary>
                /// 命令的执行
                /// </summary>
                /// <param name="args">参数数组。</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                /// <returns>命令的最终执行结果</returns>
                CommandResult Execute(string[] args, CommandContext context);

        }

        public interface ICommandWithSuggestions : IBaseCommand
        {
                /// <summary>
                /// 在输入命令参数时建议值
                /// </summary>
                /// <param name="parameter">当前参数索引</param>
                /// <param name="previous">当前已经输入的参数</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                List<string> GetSuggestions(int parameter, List<string> previous, CommandContext context);
        }
}
