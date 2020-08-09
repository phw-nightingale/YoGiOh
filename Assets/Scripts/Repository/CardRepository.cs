using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using Utility.Singleton;

namespace Repository
{
    public class CardRepository : SingletonAuto<CardRepository>
    {
        public List<BaseCardInfo> CardInfos { get; set; } = new List<BaseCardInfo>();

        public List<Card> HandleCards { get; set; } = new List<Card>();

        public CardRepository()
        {
            Debug.Log("Initializing Card Repository...");
            InitRepository();
        }

        private void InitRepository()
        {
            var json = Resources.Load<TextAsset>("Data/MonsterCard").text;
            var cards = JsonConvert.DeserializeObject<List<MonsterCardInfo>>(json);
            foreach (var card in cards)
                CardInfos.Add(card);

            json = Resources.Load<TextAsset>("Data/HandleCards").text;
            var handle = JsonConvert.DeserializeObject<List<Card>>(json);
            foreach (var card in handle)
                HandleCards.Add(card);
        }
    }

}