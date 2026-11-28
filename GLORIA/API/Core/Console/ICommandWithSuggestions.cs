using System.Collections.Generic;

namespace GLORIA.API.Core.Console
{
        public interface ICommandWithSuggestions : ICommand
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
