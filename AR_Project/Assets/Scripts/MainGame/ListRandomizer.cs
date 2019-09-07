using System;
using System.Collections.Generic;

namespace AR_Project.MainGame
{
    public static class ListRandomizer
    {
        private static Random rng = new Random(DateTime.Now.Millisecond);

        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}