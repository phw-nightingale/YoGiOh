namespace Models
{
    public enum MagicLevel
    {
        Default = 0,
        One,
        Two,
        Three,
        Four
    }

    public class Skill
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public MagicLevel Level { get; set; }

        public string Description { get; set; }
    }
}