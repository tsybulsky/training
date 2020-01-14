using System;

namespace MagicFieldGame
{
    public static class Generator
    {
        private static Random generator;
        public static Random GetGenerator()
        {
            if (generator == null)
                generator = new Random();
            return generator;
        }
    }
}
