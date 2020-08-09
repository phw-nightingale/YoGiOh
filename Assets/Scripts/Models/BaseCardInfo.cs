using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Models
{
    public enum Property
    {
        Light = '光',
        Fire = '炎',
        Earth = '地',
        Water = '水',
        Dark = '暗',
        Wind = '风',
        Magic = '魔',
        Trap = '罠'
    }

    public enum Type
    {
        Monster,
        Magic,
        Trap
    }

    public enum ShowType
    {
        // 里侧攻击表示
        InAtk,
        // 表侧攻击表示
        OutAtk,
        // 里侧守备表示
        InDef,
        // 表侧守备表示
        OutDef
    }
    
    public abstract class BaseCardInfo
    {

        private string m_Style;
        private string m_Name;
        private Property m_Property;
        private string m_Description;
        private string m_Foreground;
        private string m_Background;
        private Type m_Type;
        private ShowType m_ShowType;
        private List<Skill> m_Skills;

        public string Style
        {
            get => m_Style;
            set => m_Style = value;
        }

        public string Name
        {
            get => m_Name;
            set => m_Name = value;
        }

        public Property Property
        {
            get => m_Property;
            set => m_Property = value;
        }

        public string Description
        {
            get => m_Description;
            set => m_Description = value;
        }

        public string Details
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(m_Description)
                    .Append(";");
                foreach (var skill in m_Skills)
                    sb.Append(skill.Description)
                        .Append(";");
                return sb.ToString();
            }
        }

        public string Foreground
        {
            get => m_Foreground;
            set => m_Foreground = value;
        }

        public string Background
        {
            get => m_Background;
            set => m_Background = value;
        }

        public Type Type
        {
            get => m_Type;
            set => m_Type = value;
        }

        public ShowType ShowType
        {
            get => m_ShowType;
            set => m_ShowType = value;
        }

        public List<Skill> Skills
        {
            get => m_Skills;
            set => m_Skills = value;
        }
        
    }
}
