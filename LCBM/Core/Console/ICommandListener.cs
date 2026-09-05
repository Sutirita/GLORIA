using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCBM.Core.Console
{

        public interface ICommandListener
        {
                /// <summary>
                /// 在命令执行前调用，用于决定当前监听器是否希望拦截该命令。
                /// </summary>                 
                /// <param name="command">命令名称</param>
                /// <param name="args">参数数组。</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                /// <param name="alreadyIntercepted">指示是否已有 <b>更高优先级</b> 的监听器已声明拦截，若为true则该监听器无法拦截命令的执行。</param>
                /// <returns>返回true表示希望接拦截命令的执行；返回false则不接管。</returns>
                bool OnPreExecute(string command, string[] args, CommandContext context, bool alreadyIntercepted);

                /// <summary>
                /// 仅当此监听器接拦截命令的执行时调用，应实现自定义的命令逻辑.
                /// </summary>
                /// <param name="command">命令名称</param>
                /// <param name="args">参数数组。</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                /// <returns>命令的最终执行结果</returns>
                CommandResult Execute(string command, string[] args, CommandContext context);

                /// <summary>
                /// 在命令执行结束后调用
                /// </summary>
                /// <param name="command">命令名称</param>
                /// <param name="args">参数数组</param>
                /// <param name="result">命令的最终执行结果</param>
                /// <param name="context">表示命令执行场景的上下文</param>
                /// <param name="interceptedBy">实际执行命令的监听器实例。如果命令未被任何监听器拦截，此参数为 <c>null</c>。</param>>
                void OnPostExecute(string command, string[] args, CommandResult result, CommandContext context, ICommandListener interceptedBy);
        }
}
