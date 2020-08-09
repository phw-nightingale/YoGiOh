using System;
using UnityEngine;

namespace Utility
{
    public abstract class AbsBaseView : MonoBehaviour
    {
        public enum ViewState
        {
            Show,
            Hide
        }

        public DateTime hideTime;

        public string viewName;
        public ViewState state;

        protected virtual void Awake()
        {
            Initialize();
            InitSounds();
            InitEvents();
        }


        protected abstract void Initialize();
        protected abstract void InitSounds();
        protected abstract void InitEvents();

        public virtual void Show()
        {
            gameObject.SetActive(true);
            state = ViewState.Show;
        }

        public virtual void Hide(bool isDestroy = false)
        {
            gameObject.SetActive(false);
            state = ViewState.Hide;
            if (isDestroy)
            {
                ViewManager.Instance.Remove(viewName);
            }
        }
    }
}