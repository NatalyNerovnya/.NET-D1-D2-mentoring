﻿namespace SiteCopy
{
    using System;

    public class ConsoleNotifier : INotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }
}
