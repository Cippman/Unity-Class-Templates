using UnityEngine;

namespace CippSharp.ClassTemplates
{
    public static class StringUtility
    {
        public static string ArrayToSingleString(string[] lines, bool indempt = false)
        {
            string s = string.Empty;
            foreach (string line in lines)
            {
                if (indempt) 
                {
                    s += "\n" + line;
                } 
                else
                {
                    s += line;
                }
            }
            return s;
        }
    }
}
