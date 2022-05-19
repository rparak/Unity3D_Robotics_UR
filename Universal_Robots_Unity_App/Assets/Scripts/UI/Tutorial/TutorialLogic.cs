using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialLogic
{
    public static bool autoContinue; //Not even used lmao

    public static void Show(int id)
    {
        switch (id)
        {
            case -1: Tutorial.tutId = 7; Finish(); break;
            case 0: Start(); break;
            case 1: ConnectToRobot(); break;
            case 2: SetRobotToExternalControl(); break;
            case 3: PowerOn(); break;
            case 4: ReleaseBrakes(); break;
            case 5: WASDBewegung(); break;
            case 6: MouseControls(); break;
            case 7: Finish(); break;

            default: Tutorial.tutId = 0; Start(); break;
        }
    }


    public static void Start()
    {
        //await System.Threading.Tasks.Task.Delay(100);

        Tutorial.Show(
            "<size=50> Unser CoBot Coby sagt hallo!</size>\n\n" +
            "Willkommen zu dem Tutorial f�r <smallcaps><color=#44AACC>CoBot</color></smallcaps>, ein kollaborativer Roboter.\n\n" +
            "Du kannst jederzeit das Tutorial f�r mit dem Button <color=#44AACC>oben links</color> einblenden.\n" +
            "Klicke auf das Popup um fortzufahren."
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

        if (Robot.Connection.unityState != Robot.Connection.UnityState.offline) Robot.Connection.Disconnect();

        Tutorial.Show(
            "<size=50> Connection </size>\n\n" +
            "<color=#44AACC>Unten links</color> findest du den Connection Knopf.\n" +
            "Dieser ist entweder <color=red>Rot</color> (nicht verbunden) oder <color=green>Gr�n</color> (verbunden).\n\n" +
            "Wir m�ssen uns mit dem Roboter verbinden um ihn zu steuern.\n" +
            "In der Simulation verbinden wir uns mit dem Docker Container <mark> \"127.0.0.1\"</mark>.\n" +
            "In der Grand Garage hat der Roboter die IP <mark> \"192.168.0.102\"</mark>.\n\n" +
            "Benutze den roten Button unten um dich zu verbinden.\n" +
            "Der Grand Garage PC verbindet sich automatisch.\n" +
            "Somit musst du keine IP Adresse eingeben."
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
            "<size=50> External Control </size>\n\n" +
            "Um denn Roboter steruern zu k�nnen, m�ssen wir sicher gehen das er sich im <color=#44AACC>Remote Control Modus</color>sich befindet.\n" +
            "Dieser Schritt ist nur beim Echten Cobot wichtig.\n\n" +
            "Remote Control geht leider nur am <color=#44AACC>tablet</color>. Oben rechts steht <mark>\"local\"</mark>.\n" +
            "Der Roboter befindet sich schon im Remote Control Modus."
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
            "<size=50>Gro�artig!</size>\n\n" +
            "<color=#44AACC>Unten rechts</color> findest du den Informations Button.\n" +
            "Dort kannst du alles was zurzeit passiert.\n\n" +
            "Du bist bereit den Cobot einzuschalten.\n" +
            "Diese Einstellung findest du in dem Informations Bereich."
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
            "<size=50>Release Brakes</size>\n\n" +
            "<color=#44AACC>Der gleiche Button</color> hat sich jetzt auf <mark>\"Release Brakes\"</mark> ge�ndert.\n" +
            "Schaue auf dein Umfeld und bet�tige diese Taste nur, wenn sich keine Personen bzw. Objekte im Arbeitsbereich des Roboters befinden."
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
            "<size=50>Wie ein Spiel</size>\n\n" +
            "Wie normale Spiele ist es m�glich den CoBot mit eine <mark>\"WASD\"</mark> Steurerung zu benutzen.\n" +
            "Falls du ein Game Controller besitzt, kannst du diesen auch verwenden.\n" +
            "Worauf wartest du. Probiere das gleich aus!\n"
            );

        /*await System.Threading.Tasks.Task.Delay(30000);
        MouseControls();*/
    }

    public static void MouseControls()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Maus Steuerung</size>\n\n" +
            "Du kannst Coby auch �ber die Maus bewegen. Klicke auf den Robot um eine Achse auszuw�hlen.\n\n" +
            "Halte danach <mark>R</mark> f�r <mark>Rotate</mark> und bewege deine Maus horizontal.\n" +
            "Sobald du R gedr�ckt haltest siehst du ein <color=yellow>Gelbes Scheibe</color>.\n" +
            "Der Cobot bewegt sich dort hin, sobald du die <mark>R</mark> Taste losl�sst.\n"
            );

        /*await System.Threading.Tasks.Task.Delay(30000);
        Finish();*/
    }

    public static void Finish()
    {
        //if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Very Nice</size>\n\n" +
            "Du beherrscht Coby schon ganz gut.\n" +
            "Das Tutorial endet hier. Wir w�nschen dir Viel Spass!\n"
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
