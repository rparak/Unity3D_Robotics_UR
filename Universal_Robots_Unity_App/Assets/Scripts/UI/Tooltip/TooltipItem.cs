using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TooltipItem : MonoBehaviour
{
    private static TooltipItem Instance;

    public CanvasGroup cg;
    public RectTransform content;
    public LayoutElement layout;
    public TMP_Text text;


    public static void Show(string name, Vector3 position)
    {
        Instance.text.text = name;
        Instance.cg.LeanAlpha(1, 1f);


        //If Tooltip is in lower screen move up else down by 5%.
        if (Screen.height / 2 < position.y) position.y -= Screen.height * .06f;
        else position.y += Screen.height * .06f;


        Instance.content.pivot = new Vector2(
            position.x / Screen.width,
            position.y / Screen.height);


        Instance.content.position = position;
    }

    public static void Hide()
    {
        Instance.cg.LeanAlpha(0, 1f);
    }


    private void Start()
    {
        Instance = this;
    }

    /*private void Update()
    {
        Instance.content.position = Mouse.current.position.ReadValue();
    }*/
}
