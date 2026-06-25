using Plugify;

namespace cross_call_worker;

public class ExamplePlugin : Plugin
{
    public void OnPluginStart()
    {
        Console.WriteLine("Example: OnPluginStart");

        var strs = cross_call_worker.CallFunc33((ref a) =>
        {
            Console.WriteLine("Example: CallFunc33");
            a = "some string lul";
        });

        Console.WriteLine("Example CallFunc33: " + strs);

        var str = cross_call_worker.CallFuncEnum(
            (Example p1, ref Example[] p2) =>
            {
                Console.WriteLine("Example: CallFuncEnum");

                p1 = Example.Third;

                p2 = new[]
                {
                    Example.Third,
                    Example.Third,
                    Example.Third
                }; 

                return new[] { Example.Forth };
            }
        );

        Console.WriteLine("Example CallFuncEnum: " + string.Join(",", str));

        for (int i = 0; i < 122; i++)
        {
            cross_call_worker.NoParamReturnVoid();
        }
    }

    public void OnPluginEnd()
    {
        Console.WriteLine("Example: OnPluginEnd");
    }
}