using UnityEngine;


    public class PopupItemNoAnimation : PopupItem
    {

        public override void EscapePressed()
        {
            base.Disable();
        }

        protected override void AnimationIN()
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.LeanAlpha(1, .1f);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        protected override void AnimationOUT()
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.LeanAlpha(0, .1f);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }


