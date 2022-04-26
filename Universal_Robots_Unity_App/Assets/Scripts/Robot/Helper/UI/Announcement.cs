using System.Collections;
using System.Collections.Generic;
using Treeka;
using UnityEngine;

public class Announcement : PopupItem
{
    private static Announcement Instance;
    [SerializeField] private TMPro.TMP_Text texttext;
    

    public static void Announce(string text) => Instance.AnnounceInternal(text);

    private void AnnounceInternal(string text)
    {
        texttext.text = text;
        Enable();
        StopAllCoroutines();
        StartCoroutine(CloseAfterSecCoroutine());
    }

    private void Start()
    {
        Instance = this;
    }

    private IEnumerator CloseAfterSecCoroutine()
    {
        yield return new WaitForSecondsRealtime(3);
        Disable();
    }

    protected override void AnimationIN()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.LeanAlpha(1, .2f);
    }

    protected override void AnimationOUT()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.LeanAlpha(0, .2f);
    }
}
