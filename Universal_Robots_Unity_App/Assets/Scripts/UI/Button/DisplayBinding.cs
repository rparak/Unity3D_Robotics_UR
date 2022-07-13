using System;
using System.Linq;
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
    public string content;
    public string preTag;
    public string postTag;

    public void UpdateBinding()
    {
        if (actionRef.action.controls.Count == 0)
        {
            keybindingText.text = content;
            return;
        }

        int bindingIndex = actionRef.action.GetBindingIndexForControl(actionRef.action.controls[compositePart]);
        InputControlPath.HumanReadableStringOptions stringType = InputControlPath.HumanReadableStringOptions.OmitDevice;
        stringType |= InputControlPath.HumanReadableStringOptions.UseShortNames;

        string keyBind = InputControlPath.ToHumanReadableString(
            actionRef.action.bindings[bindingIndex].effectivePath,
            stringType);

        keybindingText.text = TranslateSprites(keyBind);
    }

    private void UpdateBindingOnControlsChange(PlayerInput player) => UpdateBinding();

    public void SetActive(bool state)
    {
        if (state)
        {
            UpdateBinding();
            //image.LeanAlphaColor(1, 0.2f);
            //if(text == null) text.LeanAlphaText(1, 0.1f);
        }
        else
        {
            //image.LeanAlphaColor(0, 0.2f);
            //if(text == null) text.LeanAlphaText(0, 0.1f);
        }
    }

    private void OnEnable() => UpdateBinding();

    private void Start() => InputTerminal.playerInput.onControlsChanged += UpdateBindingOnControlsChange;

    private void OnDestroy()
    {
        InputTerminal.playerInput.onControlsChanged -= UpdateBindingOnControlsChange;
    }


    private string TranslateSprites(string input)
    {
        if (Enum.TryParse(ToCamelCase(input), true, out Sprites sprite))
        {
            return "<sprite=" + (int)sprite + "> " + content;
        }

        return RichTextElement(input);
    }

    private string RichTextElement(string customText = "")
    {
        return preTag + " " + customText + " " + postTag + " " + content;
    }

    private enum Sprites
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
        leftStickPress = 10,

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
    
    private static string ToCamelCase(string str)
    {
        var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
        var leadWord = words[0].ToLower();
        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            .ToArray();
        return $"{leadWord}{string.Join(string.Empty, tailWords)}";
    }
}