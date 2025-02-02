using Plugify;

namespace cross_call_worker;

public class CrossCallWorker : Plugin
{
    public void OnPluginStart()
    {
        Console.WriteLine(".NET: OnPluginStart");
    }

    public void OnPluginEnd()
    {
        Console.WriteLine(".NET: OnPluginEnd");
    }
}