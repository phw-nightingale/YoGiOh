using Models;
using UnityEngine.UI;
using Utility;

namespace Views
{
    public class ItemView : AbsBaseView
    {

        public CardView cardView;

        public Image frame;

        public bool IsSelected { get; set; } = false;

        public void UpdateView(Card card)
        {
            cardView.UpdateView(card);
        }

        public void UpdateSelect(bool isSelect)
        {
            frame.gameObject.SetActive(IsSelected);
        }

        protected override void Initialize()
        {
        }

        protected override void InitSounds()
        {
        }

        protected override void InitEvents()
        {
        }

    }
}