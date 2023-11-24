using System;
using System.Collections.Generic;

namespace ThreeInRow.Back
{
    internal static class RandomElementListExtension
    {
        private static Random rnd = new Random();

        public static T GetRandomElement<T>(this IList<T> source)
        {
            int randIndex = rnd.Next(source.Count);
            return source[randIndex];
        }
    }
}
