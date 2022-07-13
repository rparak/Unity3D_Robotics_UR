using Robot;
using UnityEngine;
using UnityEngine.EventSystems;

public class JIWaypointHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup cg;
    public GameObject insideFolderBTN;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cg.LeanAlpha(1, .1f);
        cg.interactable = true;

        string parent = GetParentGuid();

        if (string.IsNullOrEmpty(parent)) insideFolderBTN.SetActive(false);
        else insideFolderBTN.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cg.LeanAlpha(0, .1f);
        cg.interactable = false;
    }

    private string GetParentGuid()
    {
        if (transform.parent.TryGetComponent<JIWaypoint>(out JIWaypoint wp)) return wp.data.parentGuid;
        if (transform.parent.TryGetComponent<JIFolder>(out JIFolder folder)) return folder.data.key;
        return string.Empty;
    }

    //JIObject is now handling this type of stuff
    /*public void AddFolder() => NewWaypointMenu.Instance.AddFolder(transform.parent.GetSiblingIndex() + 1);

    public void AddWaypoint() => NewWaypointMenu.Instance.AddWaypoint(transform.parent.GetSiblingIndex() + 1);

    public void AddWaypointInFolder()
    {
        Waypoint wp = new Waypoint(Data.Current.jointRot, Gripper.Position < 10)
        {
            parentGuid = GetParentGuid()
        };
        NewWaypointMenu.Instance.AddWaypoint(wp, transform.parent.GetSiblingIndex() + 1);
    }*/
}
