using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tip;
    public float waitTimeSec = .5f;

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ToolTipActiveCO());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TooltipItem.Hide();
    }

    private IEnumerator ToolTipActiveCO()
    {
        yield return new WaitForSecondsRealtime(waitTimeSec);
        TooltipItem.Show(tip, transform.position);
    }
}
