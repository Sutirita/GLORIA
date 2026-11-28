namespace GLORIA.API.Core.Console
{

        public interface ICommand
        {
                /// <summary>
                /// 命令的执行
                /// </summary>
                /// <param name="args">参数数组。</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                /// <returns>命令的最终执行结果</returns>
                CommandResult Execute(string[] args, CommandContext context);

        }
}
