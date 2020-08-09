using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using Utility.Singleton;

namespace Repository
{
    public class CardRepository : SingletonAuto<CardRepository>
    {
        public Dictionary<string, BaseCardInfo> CardInfoDict { get; set; } = new Dictionary<string, BaseCardInfo>();

        public Dictionary<long, Card> HandleCardDict { get; set; } = new Dictionary<long, Card>();

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
                CardInfoDict.Add(card.Style, card);

            json = Resources.Load<TextAsset>("Data/HandleCards").text;
            var handle = JsonConvert.DeserializeObject<List<Card>>(json);
            foreach (var card in handle)
                HandleCardDict.Add(card.Id, card);
        }

        public Card GetCardById(long id)
        {
            return HandleCardDict[id];
        }

        public BaseCardInfo GetCardInfoByStyle(string style)
        {
            return CardInfoDict[style];
        }
        
    }

}