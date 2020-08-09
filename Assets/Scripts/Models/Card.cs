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
    }
}