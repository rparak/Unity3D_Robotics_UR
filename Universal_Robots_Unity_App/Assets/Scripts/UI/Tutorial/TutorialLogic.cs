
public static class TutorialLogic
{
    public static bool autoContinue; //Not even used lmao

    public static void Show(int id)
    {
        switch (id)
        {
            case -2: Tutorial.tutId = 7; Finish(); break;
            case -1: ShowLayout(); break;
            case 0: Start(); break;
            case 1: ConnectToRobot(); break;
            case 2: SetRobotToExternalControl(); break;
            case 3: PowerOn(); break;
            case 4: ReleaseBrakes(); break;
            case 5: WASDBewegung(); break;
            case 6: MouseControls(); break;
            case 7: Finish(); break;

            default: Tutorial.tutId = -1; ShowLayout(); break;
        }
    }

    public static void ShowLayout()
    {
        Tutorial.Show(string.Empty);
    }

    public static void Start()
    {
        //await System.Threading.Tasks.Task.Delay(100);

        Tutorial.Show(
            "<size=50>Coby sagt hallo!</size>\n\n" +
            "Willkommen zum Tutorial unseres <smallcaps><color=#44AACC>Cobots</color></smallcaps> \n bzw. kollaborativen Roboters.\n\n" +
            "Du kannst das Tutorial jederzeit mit dem <color=#44AACC>Button oben links</color> \n ein- und ausblenden \n\n" +
            "Klicke nun weiter."
            );

        /*
            Tutorial.OnClick += Next;

            void Next()
            {
                Tutorial.OnClick -= Next;
                ConnectToRobot();
            }*/
    }

    public static void ConnectToRobot()
    {
        //await System.Threading.Tasks.Task.Delay(100);

        //if (Robot.Connection.unityState != Robot.Connection.UnityState.offline) Robot.Connection.Disconnect();

        Tutorial.Show(
            "<size=50>Verbindung</size>\n\n" +
            "<color=#44AACC>Unten links</color> wird die Verbindung zu Coby angezeigt.\n" +
            "Der Button ist entweder <color=red>Rot</color> (=nicht verbunden) oder <color=green>Grün</color> (=verbunden).\n\n" +
            "Wir müssen uns mit dem Roboter verbinden um ihn zu steuern.\n" +
            "In der Simulation verbinden wir uns mit dem Docker Container <mark> \"127.0.0.1\"</mark>.\n" +
            "In der Grand Garage hat der Roboter die IP <mark> \"192.168.0.102\"</mark>.\n\n" +
            "Verbinde dich jetzt über den roten Button unten links.\n\n" +
            "<size=25>(i) In der Grand Garage ist keine IP Eingabe notwendig. Du kannst dich also direkt verbinden.</size>"
            );

        /*
        await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.safety == Robot.Robot.RoboSafety.noRobotDetected)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }

        await System.Threading.Tasks.Task.Delay(1000);

        SetRobotToExternalControl();*/
    }

    public static void SetRobotToExternalControl()
    {
        //if (id != Tutorial.tutId) return;
        //if (await Robot.CMD.Control.IsInRemoteControl())
        {
            Tutorial.Show(
            "<size=50> Fernsteuerung </size>\n\n" +
            "Zur Fernsteuerung des echten Roboters müssen wir sicherstellen, \n dass er sich im <color=#44AACC>Remote-Control Modus</color> befindet.\n\n" +
            "Dazu müssen wir zum <color=#44AACC>Cobot-Tablet</color> wechseln. \n Oben rechts erkennst du <mark>\"local\"</mark>.\n" +
            "Hier muss auf Remote-Control umgestellt werden."
            );
            //Tutorial.OnClick += Skip;
        }
        /*else
        {
            Tutorial.Show(
            "<size=50> External Control </size>\n\n" +
            "Um denn Roboter steruern zu k�nnen, m�ssen wir sicher gehen das er sich im <color=#44AACC>Remote Control Modus</color> sich befindet.\n" +
            "Dieser Schritt ist nur beim Echten Cobot wichtig.\n\n" +
            "Remote Control geht leider nur am <color=#44AACC>tablet</color>. Oben rechts steht <mark>\"local\"</mark>.\n" +
            "Bitte auf Remote Control �ndern."
            );*/

            /*while (!await Robot.CMD.Control.IsInRemoteControl())
            {
                await System.Threading.Tasks.Task.Delay(200);
            }
            PowerOn();*/
        //}


        void Skip()
        {
            Tutorial.OnClick -= Skip;
            PowerOn();
        }
    }

    public static void PowerOn()
    {
        //if (id != Tutorial.tutId) return;
        //if (Robot.Robot.mode != Robot.Robot.RoboModes.powerOff) Robot.CMD.Control.PowerOff();

        Tutorial.Show(
            "<size=50>Großartig!</size>\n\n" +
            "<color=#44AACC>Unten rechts</color> findest du den Info-Button.\n" +
            "Dort kannst du aktuelle Statusmeldungen des Cobots sehen.\n\n" +
            "Du bist nun bereit Coby zu aktivieren.\n" +
            "Diese Einstellung findest du im erwähnten Info-Bereich."
            );

        /*await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.mode != Robot.Robot.RoboModes.idle)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }

        ReleaseBrakes();*/
    }

    public static void ReleaseBrakes()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Bremsen lösen</size>\n\n" +
            "<color=#44AACC>Der Button</color> hat sich jetzt auf <mark>\"Release Brakes\"</mark> geändert.\n\n" +
            "Pass auf deine Umgebung auf und betätige die Taste nur, \n wenn sich keine Personen und Objekte \n im Arbeitsbereich des Roboters befinden!"
            );

        /*await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.mode != Robot.Robot.RoboModes.running)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }
        await System.Threading.Tasks.Task.Delay(3000);

        WASDBewegung();*/
    }

    public static void WASDBewegung()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Spiel mit Coby!</size>\n\n" +
            "Wie bei vielen anderen Games, \n kannst du den CoBot mit <mark>\"WASD\"</mark> am Keyboard steuern.\n\n" +
            "Falls du einen Game Controller besitzt, \n kannst du auch diesen verwenden.\n\n" +
            "Worauf wartest du? Probiere es gleich aus!\n"
            );

        /*await System.Threading.Tasks.Task.Delay(30000);
        MouseControls();*/
    }

    public static void MouseControls()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Maus Steuerung</size>\n\n" +
            "Du kannst Coby auch über die Maus bewegen. \n Klicke hierzu auf ein Roboter-Gelenk um eine Achse auszuwählen.\n\n" +
            "Halte danach <mark>R</mark> für <mark>Rotation</mark> gedrückt und bewege deine Maus horizontal.\n" +
            "Nun kannst du einen <color=yellow>Winkel</color> angeben.\n\n" +
            "Sobald du <mark>R</mark> loslässt, bewegt sich der Roboter um die gewählte Achse.\n"
            );

        /*await System.Threading.Tasks.Task.Delay(30000);
        Finish();*/
    }

    public static void Finish()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Super!</size>\n\n" +
            "Sieht so aus, als ob du die Steuerung nun beherrscht.\n" +
            "Das Tutorial endet hier.\n\n Wir wünschen dir viel Spaß mit Coby!"
            );

        /*Tutorial.OnClick += Next;
        void Next()
        {
            Tutorial.OnClick -= Next;
            End();
        }*/
    }

    public static void End(int id)
    {
        //if (id != Tutorial.tutId) return;
        //Tutorial.Instance.StopTutorial();
    }
}
