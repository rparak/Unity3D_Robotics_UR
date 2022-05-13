using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class CMDTerminal
{
    public static CMDTerminal Instance;



    private List<string> PreviousCommands = new List<string>();
    private int PreviousCommandIndex = -1;

    private Queue<string> History = new Queue<string>();

    private List<string> AutoCompleteOptions = new List<string>();



    // Constructor will automatically add any commands to the command List:
    public CMDTerminal()
    {
        ConsoleCommands.FindCommands();
        Instance = this;
    }

    public ConsoleCommands.ConsoleCommand GetCommand(string CommandText)
    {
        foreach (ConsoleCommands.ConsoleCommand Command in ConsoleCommands.Commands)
        {
            if (Command.CommandText.Equals(CommandText, StringComparison.CurrentCultureIgnoreCase))
            {
                return Command;
            }
        }
        return null;
    }

    public void ExecuteCommand(string CommandText)
    {
        // safty checks:
        if (CommandText.Length == 0)
        {
            return;
        }

        CommandText = CommandText.Trim();
        PreviousCommands.Add(CommandText);
        History.Enqueue(CommandText);

        string[] SplitCommandText = CommandText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        ConsoleCommands.ConsoleCommand Command = GetCommand(SplitCommandText[0]);
        SplitCommandText = SplitCommandText.Skip(1).ToArray();
        if (Command != null)
        {
            Command.Callback(SplitCommandText);
        }
        else  // if command is invlaid, tell user and print help!
        {
            Chat.SendLocalResponse("Console", $"Can't find command: {CommandText} :/");
        }
    }

    public string AutoComplete(string AutoCompleteBase)
    {
        string AutoCompleteText = AutoCompleteBase.Trim().ToLower();

        AutoCompleteOptions.Clear();
        foreach (ConsoleCommands.ConsoleCommand Command in ConsoleCommands.Commands)
        {
            if (Command.CommandText.ToLower().StartsWith(AutoCompleteText))
            {
                AutoCompleteOptions.Add(Command.CommandText);
            }
        }
        AutoCompleteOptions.Sort();


        if (AutoCompleteOptions.Count > 0)
        {
            return AutoCompleteOptions[0];
        }


        return string.Empty;
    }

    

    public void Log(object message)
    {
        History.Enqueue(message.ToString());
    }

    public void LogWarning(object message)
    {
        History.Enqueue("Warning: " + message.ToString());
    }

    public void LogError(object message)
    {
        History.Enqueue("Error: " + message.ToString());
    }
}
