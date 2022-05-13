using System;

[AttributeUsage(AttributeTargets.Method)]
public class CMD : Attribute
{
    public string Command;
    public string HelpText;

    public CMD(string command)
    {
        Command = command;
    }

    public CMD(string command, string helptext)
    {
        Command = command;
        HelpText = helptext;
    }
}
