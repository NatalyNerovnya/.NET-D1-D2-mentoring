> Describe disadvantages for the following implementation of Singleton pattern.
> Give your solution to riddance of these shortcomings.

```csharp
public class Singleton
{
    private static Singleton instance; 

    private Singleton()
    {}
 
    public static Singleton GetInstance()
    {
        if (instance == null)
            instance = new Singleton();
        return instance;
    }
}
```

## Disadvantages of task solution
- not thred safety;
- in case of multithreading not only one instance can be created;
- class can be inherited and new instance can be created;