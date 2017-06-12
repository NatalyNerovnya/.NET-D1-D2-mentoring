namespace MvcMusicStore.Infrastructure
{
    using System.Diagnostics;

    using PerformanceCounterHelper;

    [PerformanceCounterCategory("MvcMusicStore", PerformanceCounterCategoryType.MultiInstance, "Mvc Music Store counters")]
    public enum Counters
    {
        [PerformanceCounter("Login", "Login number", PerformanceCounterType.NumberOfItems32)]
        Login,


        [PerformanceCounter("Registration", "Registration  number", PerformanceCounterType.NumberOfItems32)]
        Registration,

        [PerformanceCounter("Logoff", "Logoff  number", PerformanceCounterType.NumberOfItems32)]
        Logoff
    }
}