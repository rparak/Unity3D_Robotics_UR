using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextMeshInputHelper : MonoBehaviour
{

    public TMP_InputField tmp;
    public InputReg inputRegistery;

    private void OnEnable()
    {
        tmp.onSelect.AddListener(Select);
        tmp.onDeselect.AddListener(DeSelect);
    }

    private void OnDisable()
    {
        tmp.onSelect.RemoveListener(Select);
        tmp.onDeselect.RemoveListener(DeSelect);
    }

    private void Select(string arg0)
    {
        InputTerminal.DisableInput(inputRegistery);
    }

    private void DeSelect(string arg0)
    {
        InputTerminal.ReleaseInput(inputRegistery);
    }

}
