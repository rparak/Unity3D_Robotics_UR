using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayBinding : MonoBehaviour
{

    public InputActionReference actionRef;
    //public InputControlPath.HumanReadableStringOptions options;
    public TMP_Text keybindingText;
    public int compositePart = 0;

    [Space]
    [SerializeField] private Button button;
    public string appendingText;


    public void UpdateBinding()
    {
        if(actionRef.action.controls.Count == 0)
        {
            keybindingText.alpha = 0;
            return;
        }
        else keybindingText.alpha = 1;

        int bindingIndex = actionRef.action.GetBindingIndexForControl(actionRef.action.controls[compositePart]);
        InputControlPath.HumanReadableStringOptions stringType = InputControlPath.HumanReadableStringOptions.OmitDevice;
        stringType |= InputControlPath.HumanReadableStringOptions.UseShortNames;

        string keyBind = InputControlPath.ToHumanReadableString(
            actionRef.action.bindings[bindingIndex].effectivePath,
            stringType);

        keybindingText.text = TranslateSprites(keyBind) + " " + appendingText;
    }

    private void UpdateBindingOnControlsChange(PlayerInput player) => UpdateBinding();

    public void SetActive(bool state)
    {
        if (state)
        {
            UpdateBinding();
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        } 
    }

    private void OnEnable() => UpdateBinding();

    private void Start() => InputTerminal.playerInput.onControlsChanged += UpdateBindingOnControlsChange;
    private void OnDestroy()
    {
        InputTerminal.playerInput.onControlsChanged -= UpdateBindingOnControlsChange;
    }


    public string TranslateSprites(string key)
    {

        switch (key)
        {

            case "Circle": key = "<sprite=12>"; break;
        }


        return key;
    }

    public enum Sprites
    {
        dpad = 0,
        dpadDown = 1,
        dpadLeft = 2,
        dpadRight = 3,
        dpadUp = 4,
        dpadHorizontal = 6,
        dpadVertical = 7,

        leftTrigger = 8,
        leftBumper = 9,
        leftStick = 10,

        rightTrigger = 21,
        rightBumper = 22,
        rightStick = 24,

        a = 25,
        b = 27,
        x = 14,
        y = 17,

        square = 16,
        triangle = 19,
        cross = 14,
        circle = 12,
    }

    public enum SpritesRound
    {
        a = 26,
        b = 28,
        x = 11,
        y = 23,

        square = 18,
        triangle = 20,
        cross = 15,
        circle = 13,
    }
}

