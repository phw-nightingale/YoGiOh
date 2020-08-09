using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public abstract class AbsBaseWindow : AbsBaseView, IFadeable, IFlyable
    {
        
        public CanvasGroup panel;


        public void FadeIn()
        {
            panel.alpha = 0f;
            panel.DOFade(1.0f, 0.5f);
        }

        public void FadeOut()
        {
            panel.DOFade(0.0f, 0.5f).OnComplete(() => base.Hide());
        }

        public void FlyIn()
        {
            
        }

        public void FlyOut()
        {
            
        }

        public override void Show()
        {
            base.Show();
            FadeIn();
        }

        public override void Hide(bool isDestroy = false)
        {
            FadeOut();
        }

        protected virtual void OnClose()
        {
            
        }

    }
    
}