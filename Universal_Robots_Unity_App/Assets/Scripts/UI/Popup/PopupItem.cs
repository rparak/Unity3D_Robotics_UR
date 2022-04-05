using System;
using UnityEngine;

namespace Treeka
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupItem : MonoBehaviour
    {
        public static event Action OnPopupChange;

        protected Transform oldParent;


        public virtual void Enable()
        {
            oldParent = transform.parent;
            transform.SetParent(Popup.popupParent);

            OnPopupChange?.Invoke();
            AnimationIN();
        }

        public virtual void EscapePressed() {}

        public virtual void Disable()
        {
            transform.SetParent(oldParent);

            OnPopupChange?.Invoke();
            AnimationOUT();
        }


        // /////////////////////

        protected virtual void AnimationIN()
        {
            transform.localScale = Vector3.one * 10;
            LeanTween.scale(gameObject, Vector3.one, .1f);
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.LeanAlpha(1, .1f);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        protected virtual void AnimationOUT()
        {
            LeanTween.scale(gameObject, Vector3.one * 10, .1f);
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.LeanAlpha(0, .1f);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
}


