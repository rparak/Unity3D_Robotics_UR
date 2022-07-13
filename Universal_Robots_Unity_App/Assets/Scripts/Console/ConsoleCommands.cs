using System;
using System.Collections.Generic;
using System.Reflection;

public static class ConsoleCommands
{
	public static List<ConsoleCommand> Commands = new List<ConsoleCommand>();

	public delegate void Command(string[] Params);

	public class ConsoleCommand
	{
		public ConsoleCommand(string CommandText, string HelpText, Command Callback)
		{
			this.CommandText = CommandText;
			this.HelpText = HelpText;
			this.Callback = Callback;
		}

		public string CommandText;
		public string HelpText;
		public Command Callback;
	}

	// finds any marked commands in the current assembly
	// and adds them to the commands list!
	static public void FindCommands()
	{
		CMD cmd;

		Assembly currAssembly = Assembly.GetCallingAssembly();

		foreach (Type type in currAssembly.GetTypes())
		{
			// check the methods in every class:
			if (type.IsClass)
			{
				foreach (MethodInfo method in type.GetMethods())
				{
					// if the method meets our requirments for a console command (i.e. is buplic and static).
					if (method.IsPublic && method.IsStatic)
					{
						// check its attributes:
						foreach (Attribute attr in method.GetCustomAttributes(true))
						{
							cmd = attr as CMD;

							if (cmd != null)
							{
								// create and the command based on its attribute data.
								ConsoleCommand command = new ConsoleCommand(cmd.Command, cmd.HelpText, Delegate.CreateDelegate(typeof(Command), method) as Command);
								
								if (command != null)
								{
									if (Commands.Contains(command) == false)
									{
										Commands.Add(command);
									}
								}
							}
						} // end attrib loop.
					}
				} // end method loop
			}
		} // end Class loop.
	}



	[CMD("Help", "Prints the Debug Console help text")]
	public static void GetHelp(string[] Params)
	{
		string helpText = "";

        if (Params.Length == 0)
        {
			foreach (ConsoleCommand command in Commands)
			{
				helpText = helpText + command.CommandText + " | " + command.HelpText + "\n";
			}
		}
        else
        {
			Params[0].ToLower();
			foreach (ConsoleCommand command in Commands)
			{
				if(command.CommandText.ToLower() == Params[0])
                {
					helpText = command.HelpText;
					break;
                }
			}
			if (helpText == string.Empty) helpText = $"Could not find the command {Params[0]}";
		}

		Chat.SendLocalResponse("Console", helpText);
	}

	[CMD("Freedrive", "Is a test cmd for freedrive")]
	public static void SetFreedrive(string[] Params)
    {
		Robot.CMD.FreeDrive();
    }
}
