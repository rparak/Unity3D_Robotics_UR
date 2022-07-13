using UnityEngine;

public class JIDropdownHelper : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public void Click() => transform.parent.GetComponent<JIDropdown>().NewValue(text.text);
}