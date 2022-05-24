
public static class RobotConsoleCMD
{
    [CMD("RobotSend", "Send a Command to the SendPort")]
    public static void RobotSend(string[] param)
    {
        string command = string.Empty;
        foreach (var word in param)
        {
            command += word + " ";
        }
        command = command.Trim();
        Robot.Connection.SendCommand(command + "\n");
        Chat.SendLocalResponse("RobotCMD", "Send");
    }

    [CMD("RobotDash", "Send a Command to the DashPort")]
    public async static void RobotDash(string[] param)
    {
        string command = string.Empty;
        foreach (var word in param)
        {
            command += word + " ";
        }
        command = command.Trim();
        string info = await Robot.Connection.SendDashboardAsync(command + "\n");
        Chat.SendLocalResponse("RobotCMD", info);
    }

    [CMD("RobotGripper", "Send a Command to the GripperPort")]
    public async static void RobotGripper(string[] param)
    {
        string command = string.Empty;
        foreach (var word in param)
        {
            command += word + " ";
        }
        command = command.Trim();
        string info = await Robot.Connection.SendGripper(command + "\n");
        Chat.SendLocalResponse("RobotCMD", info);
    }
}
