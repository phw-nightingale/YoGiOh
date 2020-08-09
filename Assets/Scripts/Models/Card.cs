using UnityEngine.Events;

namespace Models
{
    public class Card
    {
        public long Id { get; set; }

        public string Style { get; set; }

        public Card()
        {
        }

        public Card(long id, string style)
        {
            Id = id;
            Style = style;
        }

        public UnityAction<Card> OnValueChanged;

        public override string ToString()
        {
            return $"Card[Id: {Id}, Style: {Style}]";
        }
    }
}