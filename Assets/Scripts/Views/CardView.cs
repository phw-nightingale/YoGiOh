using Models;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Views
{
    public class CardView : AbsBaseView
    {

        public Card Card { get; set; }

        public Image foreground;

        public void UpdateView(Card card)
        {
            var texture = Resources.Load<Texture2D>($"Cards/Images/{card.Style}");
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            foreground.sprite = sprite;
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