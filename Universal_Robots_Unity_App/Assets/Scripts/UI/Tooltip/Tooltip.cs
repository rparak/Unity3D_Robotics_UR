using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tip;
    public const float waitTimeSec = .1f;

    

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
