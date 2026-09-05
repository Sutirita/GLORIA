using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LCBM.Core.Console
{
        /// <summary>
        /// 命令的执行结果
        /// </summary>
        public enum CommandResult
        {
                Success,
                BadSyntax,
                WrongContext,
                Error
        }
        /// <summary>
        /// 用于表示命令执行场景的上下文
        /// </summary>
        public enum CommandContext
        {
                None,
                MainMenu,
                Management,
                Story,
        }


        public struct LogEntry
        {
                public string message;
                public Color color;
                public int fontSize;
                public bool bold;

                public LogEntry(string msg, Color col, int size = 24, bool bold = false)
                {
                        message = msg;
                        color = col;
                        fontSize = size;
                        this.bold = bold;
                }
        }





        public sealed class Console
        {
                private static Console _instance;
                public static Console Instance
                {
                        get
                        {
                                if (_instance is null)
                                {
                                        _instance = new Console();
                                }
                                return _instance;
                        }
                }


                public List<LogEntry> LogEntrys = new List<LogEntry>();

                private Stack<string> InputHistoryStackA = new Stack<string>();

                private Stack<string> InputHistoryStackB = new Stack<string>();


                private CommandContext _commandContext;

                public CommandContext CurrentContext => _commandContext;




                private static CommandResult RunCommand(string input, CommandContext context)
                {
                        return CommandResult.Success;
                }




                public string ViewInputHistoryUP()
                {
                        if (InputHistoryStackA.Count == 0) return "";
                        string value = InputHistoryStackA.Pop();
                        InputHistoryStackB.Push(value);
                        return value;
                }
                public string ViewInputHistoryDown()
                {
                        if (InputHistoryStackB.Count == 0) return "";
                        string value = InputHistoryStackB.Pop();
                        InputHistoryStackA.Push(value);
                        return value;
                }

                private void UpdateInputHistory(string value)
                {
                        while (InputHistoryStackB.Count > 0)
                        {
                                string item = InputHistoryStackB.Pop();
                                InputHistoryStackA.Push(item);
                        }
                        InputHistoryStackA.Push(value);
                }



                public void Log(string message, Color color, int fontSize = 24, bool bold = false)
                {
                        LogEntrys.Add(new LogEntry(message, color, fontSize, bold));
                }

                public void Log(string message) // 默认白色
                {
                        Log(message, Color.white);
                }




                public void OnSubmit(string value)
                {

                        Log(value);

                        //RunCommand()

                        UpdateInputHistory(value);

                }

        }








}
