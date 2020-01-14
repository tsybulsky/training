using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class FuzzyStringCompare
    {
        private static (int,int) MatchingStrings(string value1, string value2, int len)
        {
            int likeCount = 0;
            int subRows = 0;
            for (int i = 0; i <= value1.Length-len;i++ )
            {
                string tempValue1 = value1.Substring(i, len);
                for (int j = 0; j <= value2.Length-len;j++)
                {
                    string tempValue2 = value2.Substring(j, len);
                    if (tempValue2 == tempValue1)
                        likeCount++;
                }
                subRows++;
            }
            return (likeCount, subRows);
        }

        private static string RemoveInvalidChars(string value)
        {
            StringBuilder result = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsLetter(value[i]))
                    result.Append(value[i]);
            }
            return result.ToString();
        }

        private static string[] RemoveLittleWords(string[] words)
        {
            string[] result = new string[words.Length];
            int newSize = 0;
            for (int i = 0; i < words.Length;i++)
            {
                string temp = RemoveInvalidChars(words[i]);
                if ((temp.Length > 2)||(temp == "не"))
                    result[newSize++] = temp.ToLower();
            }
            Array.Resize(ref result, newSize);
            return result;
        }

        private static int CompareWords(string value1, string value2, int maxLen)
        {
            int likeCount = 0;
            int subRows = 0;
            for (int i = 1; i <= maxLen; i++)
            {
                (int, int) result = MatchingStrings(value1, value2, i);
                likeCount += result.Item1;
                subRows += result.Item2;
                result = MatchingStrings(value2, value1, i);
                likeCount += result.Item1;
                subRows += result.Item2;
            }
            return (likeCount * 100 / subRows);
        }
        private static string[] RemoveDublicates(string[] items)
        {
            int newSize = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                    if (items[i] == items[j])
                    {
                        items[i] = "";
                        newSize--;
                    }
            }
            string[] result = new string[newSize];            
            for (int i = 0, index=0; i < items.Length; i ++)
            {
                if (items[i] != "")
                    result[index++] = items[i];
            }
            return result;
        }
        public static int IndistinctMatching(this string value1, string value2, int threshold = 75)
        {
            if (String.IsNullOrEmpty(value1) || String.IsNullOrEmpty(value2))
                return 0;
            string[] words1 = RemoveLittleWords(value1.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            words1 = RemoveDublicates(words1);
            string[] words2 = RemoveLittleWords(value2.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            words2 = RemoveDublicates(words2);
            if ((words1.Length == 0) || (words2.Length == 0))
                return 0;            
            int mark = 0;
            for (int i = 0; i < words1.Length; i++)
            {
                for (int j=0; j < words2.Length; j++)
                {
                    mark += CompareWords(words1[i], words2[j], Math.Min(words1[i].Length, words2[j].Length));                    
                }
            }
            return mark / ((words1.Length+words2.Length)/2);
        }
    }
}
