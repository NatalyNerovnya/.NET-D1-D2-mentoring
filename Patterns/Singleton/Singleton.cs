namespace Singleton
{
    using System;

    public sealed class Singleton
    {
        private static Singleton instance;
        private static object syncRoot = new Object();

        private Singleton()
        { }

        public static Singleton GetInstance()
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new Singleton();
            }
            return instance;
        }
    }
}
