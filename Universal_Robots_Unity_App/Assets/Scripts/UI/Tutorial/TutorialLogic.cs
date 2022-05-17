using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialLogic
{

    public async static void Start(int id)
    {
        await System.Threading.Tasks.Task.Delay(100);

        Tutorial.Show(
            "<size=50> Unser Cobot Coby sagt hallo! </size>\n\n" +
            "Willkommen zu die einführung userem<smallcaps> <color=#44AACC>Cobot</color></smallcaps>.\n\n" +
            "Du kannst jederzeit<color=#44AACC> oben links</color> das Tutorial Beenden oder dieses Popup wieder anschauen\n" +
            "indem du auf die Grüne Info taste oben drauf drückst.\n\n" +
            "Klicke auf dieses Popup um weiter zu gehen."
            );

        Tutorial.OnClick += Next;

        void Next()
        {
            Tutorial.OnClick -= Next;
            ConnectToRobot(id);
        }
    }

    public static async void ConnectToRobot(int id)
    {
        if (id != Tutorial.tutId) return;
        await System.Threading.Tasks.Task.Delay(100);

        if (Robot.Connection.unityState != Robot.Connection.UnityState.offline) Robot.Connection.Disconnect();

        Tutorial.Show(
            "<size=50> Connection </size>\n\n" +
            "<color=#44AACC>Unten Links</color> findest du den Connection Knopf.\n" +
            "Dieser ist entweder <color=red>Rot</color>(Nicht Verbunden) oder <color=green>Grün</color>(Verbunden).\n\n" +
            "Wir müssen uns erst mit dem Roboter Verbinden um ihn zu steuern.\n" +
            "In der Simulation verbinden wir uns mit dem Docker container <mark> \"127.0.0.1\" </mark>.\n" +
            "In der Grand Garage hat der Roboter die IP <mark> \"192.168.0.102\" </mark>.\n\n" +
            "Benutze den Roten Knopf unten um dich zu verbinden.\n" +
            "Der Grand Garage Pc verbindet sich automatisch\n" +
            "somit musst du keine IP adresse eingeben."
            );


        await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.safety == Robot.Robot.RoboSafety.noRobotDetected)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }

        await System.Threading.Tasks.Task.Delay(1000);

        SetRobotToExternalControl(id);
    }

    public static async void SetRobotToExternalControl(int id)
    {
        if (id != Tutorial.tutId) return;
        if (await Robot.CMD.Control.IsInRemoteControl())
        {
            Tutorial.Show(
            "<size=50> External Control </size>\n\n" +
            "Um denn Roboter steruern zu können, müssen wir sicher gehen das er sich im <color=#44AACC>Remote Control Modus</color>sich befindet.\n" +
            "Dieser Schritt ist nur beim Echten Cobot wichtig.\n\n" +
            "Remote Control geht leider nur am <color=#44AACC>tablet</color>. Oben rechts steht <mark>\"local\"</mark>.\n" +
            "Der Roboter befindet sich schon im Remote Control Modus."
            );
            Tutorial.OnClick += Skip;
        }
        else
        {
            Tutorial.Show(
            "<size=50> External Control </size>\n\n" +
            "Um denn Roboter steruern zu können, müssen wir sicher gehen das er sich im <color=#44AACC>Remote Control Modus</color> sich befindet.\n" +
            "Dieser Schritt ist nur beim Echten Cobot wichtig.\n\n" +
            "Remote Control geht leider nur am <color=#44AACC>tablet</color>. Oben rechts steht <mark>\"local\"</mark>.\n" +
            "Bitte auf Remote Control ändern."
            );

            while (!await Robot.CMD.Control.IsInRemoteControl())
            {
                await System.Threading.Tasks.Task.Delay(200);
            }
            PowerOn(id);
        }


        void Skip()
        {
            Tutorial.OnClick -= Skip;
            PowerOn(id);
        }
    }

    public static async void PowerOn(int id)
    {
        if (id != Tutorial.tutId) return;
        if (Robot.Robot.mode != Robot.Robot.RoboModes.powerOff) Robot.CMD.Control.PowerOff();

        Tutorial.Show(
            "<size=50>Großartig!</size>\n\n" +
            "<color=#44AACC>Unten Rechts</color> findest du den Informations Bildschirim.\n" +
            "Dort kannst siehst du alles was zurzeit passiert.\n\n" +
            "Du bist bereit denn Cobot einzuschalten.\n" +
            "Diese einstellung findest du in dem Informations Bildshirm."
            );

        await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.mode != Robot.Robot.RoboModes.idle)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }

        ReleaseBrakes(id);
    }

    public static async void ReleaseBrakes(int id)
    {
        if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Release Brakes</size>\n\n" +
            "<color=#44AACC>Der gleiche Knopf</color> hat sich jetzt auf Release Brakes geändert.\n" +
            "Schaue auf dein Umfeld und betätige diese Taste nur wenn du und keine anderen Objecte weg vom Cobot sich befinden."
            );

        await System.Threading.Tasks.Task.Delay(1000);
        while (Robot.Robot.mode != Robot.Robot.RoboModes.running)
        {
            await System.Threading.Tasks.Task.Delay(200);
        }
        await System.Threading.Tasks.Task.Delay(3000);

        WASDBewegung(id);
    }

    public static async void WASDBewegung(int id)
    {
        if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Wie ein Spiel</size>\n\n" +
            "Wie normale spiele ist es möglich denn Cobot mit <mark>\"WASD\"</mark> stererung zu benutzen.\n" +
            "Falls du ein Controller hast, kannst du diesen auch verwenden.\n" +
            "Worauf wartest du. Probiere das gleich aus!\n"
            );

        await System.Threading.Tasks.Task.Delay(30000);
        MouseControls(id);
    }

    public static async void MouseControls(int id)
    {
        if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Maus steuerung</size>\n\n" +
            "Du kannst Coby auch über die Maus Bewegen. Klicke auf den Robot um ein Axis auszuwählen.\n\n" +
            "Halte danach <mark>R</mark> für <mark>Rotate</mark> und bewege deine Maus horizontal.\n" +
            "Sobald du R gedrückt haltest siehst du ein <color=yellow>Gelbes Rad</color>.\n" +
            "Der Cobot bewegt sich dort hin sobald du die <mark>R</mark> taste auslässt.\n"
            );

        await System.Threading.Tasks.Task.Delay(30000);
        Finish(id);
    }

    public static void Finish(int id)
    {
        if (id != Tutorial.tutId) return;
        Tutorial.Show(
            "<size=50>Very Nice</size>\n\n" +
            "Du beherscht Coby schon ganz gut.\n" +
            "Wie mit der Maus steuerung kannst du den Gripper auch leicht bedienen.\n" +
            "Fallst du noch nicht mit die Controls klar kommst kannst du Jederzeit im <color=#44AACC>Oben Info Bildschirm</color> nachschauen.\n\n" +
            "Das Tutorial endet hier. Wir wünschen dir Viel Spass!\n"
            );

        Tutorial.OnClick += Next;
        void Next()
        {
            Tutorial.OnClick -= Next;
            End(id);
        }
    }

    public static void End(int id)
    {
        if (id != Tutorial.tutId) return;
        Tutorial.Instance.StopTutorial();
    }
}
