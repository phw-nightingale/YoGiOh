using System.Collections.Generic;

namespace Models
{
    public class MonsterCardInfo : BaseCardInfo
    {
        
        public enum MonsterRace
        {
            Warrior,
            OrcWarrior,
            Insect,
            Undead,
            Dragon,
            Magician
            
        }
        
        public enum Tag
        {
            Effect,
            Ceremony,    // 仪式
            Fuse,    // 融合     
            Homology,    // 同调
            DarkHomology,    // 黑暗同调
            Adjust,    // 调整
            Double,    // 二重
            Unity,    // 同盟
            Soul,    // 灵魂
            Cartoon    // 卡通
        }

        public string Prefab { get; set; }

        public int Level { get; set; }

        public MonsterRace Race { get; set; }

        public int Atk { get; set; }

        public int Def { get; set; }

        public List<Tag> Tags { get; set; }
    }
}