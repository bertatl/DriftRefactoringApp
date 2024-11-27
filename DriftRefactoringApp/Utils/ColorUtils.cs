using System;

namespace DriftRefactoringApp.Utils
{
    public class ColorUtils
    {
        private int seedValue;
        private string somethingValue;  // the absolutely useless field that's used just for illustration
        public ColorUtils(int seed)
        {
            seedValue = seed;
        }
        public ColorUtils(int seed, string something)
        {
            // This is to illustrate constructor with multiple parameters + overloading
            somethingValue = something;
            seedValue = seed;
        }
        public ColorUtils(string something)
        {
            // This is to illustrate constructor overloading
            somethingValue = something;
        }

        public int PickRandomNumber(int limit)
        {
            var random = new Random();
            var index = random.Next(limit) + 1;
            return index;
        }
        public int PickRandomNumberRange(int min, int max)
        {
            // This is to illustrate method with multiple parameters
            // seedValue initialized as a part of ctor is only for illustration
            // It's better not to instantiate Random with that for true randomness.
            var random = new Random();
            var index = random.Next(min, max) + 1;
            return index;
        }
        public int PickRandomNumberRange(int min, int max, string something)
        {
            // This is to illustrate method overloading
            return PickRandomNumberRange(min, max);
        }
        public int PickRandomNumber(string notExposed)
        {
            // This is to illustrate an overloaded method that should not be exposed
            return -1;
        }
        public void PickRandomNumberAndDontComeBack(int limit)
        {
            // This is to illustrate a void function that can be exposed
            return;
        }
    }
}