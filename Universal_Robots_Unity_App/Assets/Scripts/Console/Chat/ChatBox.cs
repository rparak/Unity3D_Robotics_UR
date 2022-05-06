using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class ChatBox : Treeka.PopupItem
{
    [SerializeField] InputActionReference tabAction;

    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text autoCommandSuggestion;


    private void OnSubmit(string message)
    {
        inputField.text = string.Empty;
        Disable();

        if (!inputField.wasCanceled)
        {
            CMDTerminal.Instance.ExecuteCommand(message);
        }
    }

    private void OnWriting(string uncompleteMessage)
    {
        autoCommandSuggestion.text = $"{CMDTerminal.Instance.AutoComplete(uncompleteMessage).ToLower()}";
    }

    private void OnCancel(string whyWeNeedThis)
    {
        inputField.text = string.Empty;
        Disable();
    }



    private void ComepleteSuggestedCommand(InputAction.CallbackContext ctx)
    {
        if (string.IsNullOrEmpty(inputField.text)) return;
        string command = inputField.text;
        inputField.text = $"{CMDTerminal.Instance.AutoComplete(command).ToLower()}";
        inputField.MoveTextEnd(false);
    }

    // ///////////////////////

    public void ActivateCMDChat(InputAction.CallbackContext ctx)
    {
        Enable();
    }

    IEnumerator SelectInputField()
    {
        yield return new WaitForEndOfFrame();
        inputField.ActivateInputField();
    }

    // /////////////////////////


    private void OnEnable()
    {
        inputField.onSubmit.AddListener(OnSubmit);
        inputField.onValueChanged.AddListener(OnWriting);
        inputField.onEndEdit.AddListener(OnCancel);
        tabAction.action.performed += ComepleteSuggestedCommand;
        autoCommandSuggestion.text = string.Empty;
    }


    private void OnDisable()
    {
        inputField.onSubmit.RemoveAllListeners();
        inputField.onValueChanged.RemoveAllListeners();
        inputField.onEndEdit.RemoveAllListeners();
        tabAction.action.performed -= ComepleteSuggestedCommand;
    }


    // ///////////////////////////////////

    public override void Enable()
    {
        base.Enable();
        Chat.Show();
        StartCoroutine(SelectInputField());
    }

    public override void Disable()
    {
        base.Disable();
        Chat.Hide();
    }
}
