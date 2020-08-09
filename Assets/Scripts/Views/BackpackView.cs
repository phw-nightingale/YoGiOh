using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Views
{
    public class BackpackView : AbsBaseView
    {

        public Transform content;

        public ToggleGroup toggles;

        private List<ItemView> m_Items = new List<ItemView>();

        public ScrollRect scroll;

        private void Start()
        {
            InitializeGrid();
            scroll.onValueChanged.AddListener(v2 => Debug.Log(v2));
        }

        public void UpdateView()
        {
            
        }

        private void InitializeGrid()
        {
            foreach (var map in CardRepository.Instance.HandleCardDict)
            {
                var item = Resources.Load<GameObject>("Prefabs/Card/Item");
                item = Instantiate(item, content);
                var itemView = item.GetComponent<ItemView>();
                itemView.cardView.Card = map.Value;
                itemView.cardView.UpdateView(map.Value);
                m_Items.Add(itemView);
            }
        }

        public void UpdateSelectItem(Card card)
        {
            foreach (var item in m_Items)
            {
                item.IsSelected = false;
            }

            if (card != null)
            {
                foreach (var item in m_Items.Where(item => item.cardView.Card.Id.Equals(card.Id)))
                {
                    item.IsSelected = true;
                    break;
                }
            }
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