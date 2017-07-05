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