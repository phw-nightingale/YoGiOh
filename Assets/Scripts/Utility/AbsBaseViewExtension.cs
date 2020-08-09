using System;

namespace Utility
{
    public static class AbsBaseViewExtension
    {
        
        public static void GetComponents(this AbsBaseView view)
        {
            Type type = typeof(AbsBaseView);
            foreach (var attr in type.GetCustomAttributes(true))
            {
                AttributeName attrName = (AttributeName) attr;
                if (attrName != null)
                {
                    string componentName = attr.GetType().ToString();
                    //attr = view.GetComponent(attr.GetType(), componentName)
                }
            }
        }
        
    }
}