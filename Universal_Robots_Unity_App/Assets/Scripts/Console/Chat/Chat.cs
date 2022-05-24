using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public static Chat Instance;
    public static CMDTerminal cmdTerminal;
    [SerializeField] GameObject messageObject;
    [SerializeField] Transform messageParent;
    [SerializeField] VerticalLayoutGroup layout;




    public static void SendLocalResponse(string author, string message)
    {
        ChatMessage chatMessage = Instantiate(Instance.messageObject, Instance.messageParent).GetComponent<ChatMessage>();
        chatMessage.Create($"<color=red>{author}</color>", message);
        messages.Add(chatMessage);
    }


    public Chat() => Instance = this;

    public enum MessageType
    {
        All,
        Team,
        System
    }




    [SerializeField] ChatBox chatbox;
    [SerializeField] InputActionReference cmdChatAction;


    private void Start()
    {
        cmdChatAction.action.performed += chatbox.ActivateCMDChat;
        cmdTerminal = new CMDTerminal();
    }

    private void OnDestroy()
    {
        cmdChatAction.action.performed -= chatbox.ActivateCMDChat;
    }





    //Chat Resolver ----------------------

    static List<ChatMessage> messages = new List<ChatMessage>();

    public static void Show()
    {
        foreach (var message in messages) message.Show();
    }

    public static void Hide()
    {
        foreach (var message in messages) message.Hide();
    }

    public void Clear()
    {
        foreach (var message in messages)
        {
            Destroy(message.gameObject);
        }
        messages.Clear();
    }

    [CMD("chatclear", "removes all messages from the console")]
    public static void ClearChat(string[] args)
    {
        Instance.Clear();
    }
}
