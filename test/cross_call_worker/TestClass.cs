using System;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using cross_call_master;

namespace cross_call_worker;

public class TestClass
{
    [Conditional("VERBOSE")]
    static void Log(string message)
    {
        Console.WriteLine(message);
    }
    
    public static string BasicLifecycle()
    {
        Log("TEST 1: Basic Lifecycle");
        Log("_______________________");
        
        int initialAlive = ResourceHandle.GetAliveCount();
        int initialCreated = ResourceHandle.GetTotalCreated();
        
        using (var resource = new ResourceHandle(1, "Test1"))
        {
            Log($"v Created ResourceHandle ID: {resource.GetId()}");
            Log($"v Alive count increased: {ResourceHandle.GetAliveCount()}");
        }
        
        int finalAlive = ResourceHandle.GetAliveCount();
        int finalCreated = ResourceHandle.GetTotalCreated();
        int finalDestroyed = ResourceHandle.GetTotalDestroyed();
        
        Log($"v Destructor called, alive count: {finalAlive}");
        Log($"v Total created: {finalCreated - initialCreated}");
        Log($"v Total destroyed: {finalDestroyed}");
        
        if (finalAlive == initialAlive && finalCreated == finalDestroyed)
        {
            Log("v TEST 1 PASSED: Lifecycle working correctly\n");
            return "true";
        }
        else
        {
            Log("x TEST 1 FAILED: Lifecycle mismatch!\n");
            return "false";
        }
    }
    
    public static string StateManagement()
    {
        Log("TEST 2: State Management");
        Log("________________________");
        
        using (var resource = new ResourceHandle(2, "StateTest"))
        {
            // Test counter
            resource.IncrementCounter();
            resource.IncrementCounter();
            resource.IncrementCounter();
            int counter = resource.GetCounter();
            Log($"v Counter incremented 3 times: {counter}");
            
            // Test name change
            resource.SetName("StateTestModified");
            string newName = resource.GetName();
            Log($"v Name changed to: {newName}");
            
            // Test data storage
            resource.AddData(1.1f);
            resource.AddData(2.2f);
            resource.AddData(3.3f);
            float[] data = resource.GetData();
            Log($"v Added {data.Length} data points");
            
            if (counter == 3 && newName == "StateTestModified" && data.Length == 3)
            {
                Log("v TEST 2 PASSED: State management working\n");
                return "true";
            }
            else
            {
                Log("x TEST 2 FAILED: State not preserved!\n");
                return "false";
            }
        }
    }
    
    public static string MultipleInstances()
    {
        Log("TEST 3: Multiple Instances");
        Log("__________________________");
        
        int beforeAlive = ResourceHandle.GetAliveCount();
        
        using (var r1 = new ResourceHandle(10, "Instance1"))
        using (var r2 = new ResourceHandle(20, "Instance2"))
        using (var r3 = new ResourceHandle())  // Auto-generated ID
        {
            int duringAlive = ResourceHandle.GetAliveCount();
            Log($"v Created 3 instances, alive: {duringAlive}");
            Log($"v R1 ID: {r1.GetId()}, R2 ID: {r2.GetId()}, R3 ID: {r3.GetId()}");
            
            if (duringAlive - beforeAlive == 3)
            {
                Log("v All 3 instances tracked correctly");
            }
        }
        
        int afterAlive = ResourceHandle.GetAliveCount();
        
        if (afterAlive == beforeAlive)
        {
            Log("v TEST 3 PASSED: All instances destroyed properly\n");
            return "true";
        }
        else
        {
            Log($"x TEST 3 FAILED: Leak detected! Before: {beforeAlive}, After: {afterAlive}\n");
            return "false";
        }
    }
    
    public static string CounterWithoutDestructor()
    {
        Log("TEST 4: Counter (No Destructor)");
        Log("________________________________");
        
        // No using statement needed - Counter doesn't implement IDisposable
        var counter = new Counter(100);
        Log($"v Created Counter with value: {counter.GetValue()}");
        
        counter.Increment();
        counter.Increment();
        counter.Add(50);
        long value = counter.GetValue();
        Log($"v After operations, value: {value}");
        
        bool isPositive = counter.IsPositive();
        Log($"v Is positive: {isPositive}");
        
        if (value == 152 && isPositive)
        {
            Log("v TEST 4 PASSED: Counter operations working\n");
            return "true";
        }
        else
        {
            Log("x TEST 4 FAILED: Counter operations incorrect\n");
            return "false";
        }
        
        // Note: Counter will be cleaned up by GC, no destructor call
    }
    
    public static string StaticMethods()
    {
        Log("TEST 5: Static Methods");
        Log("______________________");
        
        // ResourceHandle static methods
        int alive = ResourceHandle.GetAliveCount();
        int created = ResourceHandle.GetTotalCreated();
        int destroyed = ResourceHandle.GetTotalDestroyed();
        Log($"v ResourceHandle stats - Alive: {alive}, Created: {created}, Destroyed: {destroyed}");
        
        // Counter static methods
        int cmp1 = Counter.Compare(100, 50);
        int cmp2 = Counter.Compare(50, 100);
        int cmp3 = Counter.Compare(50, 50);
        Log($"v Counter.Compare(100, 50) = {cmp1} (expected 1)");
        Log($"v Counter.Compare(50, 100) = {cmp2} (expected -1)");
        Log($"v Counter.Compare(50, 50) = {cmp3} (expected 0)");
        
        long sum = Counter.Sum(new long[] { 1, 2, 3, 4, 5 });
        Log($"v Counter.Sum([1,2,3,4,5]) = {sum} (expected 15)");
        
        if (cmp1 == 1 && cmp2 == -1 && cmp3 == 0 && sum == 15)
        {
            Log("v TEST 5 PASSED: Static methods working\n");
            return "true";
        }
        else
        {
            Log("x TEST 5 FAILED: Static methods incorrect\n");
            return "false";
        }
    }

    public static string MemoryLeakDetection()
    {
        Log("TEST 6: Memory Leak Detection");
        Log("______________________________");

        int beforeAlive = ResourceHandle.GetAliveCount();

        {
            // Intentionally don't use 'using' and don't call Dispose()
            // This tests that finalizers work as a safety net
            var leaked = new ResourceHandle(999, "IntentionalLeak");
            Log($"v Created resource ID: {leaked.GetId()}");
        }
        
        // Force GC to run finalizers
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        
        int afterAlive = ResourceHandle.GetAliveCount();
        
        Log($"v Before leak test: {beforeAlive} alive");
        Log($"v After GC: {afterAlive} alive");
        
        if (afterAlive == beforeAlive)
        {
            Log("v TEST 6 PASSED: Finalizer cleaned up leaked resource\n");
            return "true";
        }
        else
        {
            Log("x TEST 6 FAILED: Resource still alive (will be cleaned at plugin shutdown)\n");
            return "false";
        }
    }
    
    public static string ExceptionHandling()
    {
        Log("TEST 7: Exception Handling");
        Log("__________________________");
        
        var resource = new ResourceHandle(777, "ExceptionTest");
        resource.Dispose();
        
        try
        {
            // This should throw ObjectDisposedException
            resource.GetId();
            Log("x TEST 7 FAILED: No exception thrown!\n");
            return "false";
        }
        catch (ObjectDisposedException ex)
        {
            Log($"v Caught expected exception: {ex.GetType().Name}");
            Log("v TEST 7 PASSED: Exception handling working\n");
            return "true";
        }
    }
    
    public static unsafe string OwnershipTransfer() 
    {
        Log("TEST 7: Ownership Transfer (get + release)");
        Log("─────────────────────────────────────────");
    
        int initialAlive = ResourceHandle.GetAliveCount();
        int initialCreated = ResourceHandle.GetTotalCreated();
    
        var resource = new ResourceHandle(42, "OwnershipTest");
        Log($"✓ Created ResourceHandle ID: {resource.GetId()}");
    
        // Get internal wrapper (simulate internal pointer access)
        var wrapper = resource.Get();
        Log($"✓ get() returned internal wrapper: {wrapper.GetHashCode():X}");
    
        // Release ownership
        var handle = resource.Release();
        Log($"✓ release() returned handle: {handle.GetHashCode():X}");
    
        if (wrapper != handle) 
        {
            Log("✗ TEST 7 FAILED: get() did not return internal wrapper");
            return "false";
        }
    
        try 
        {
            resource.GetId();
            Log("✗ TEST 7 FAILED: ResourceHandle still accessible after release()");
            return "false";
        } 
        catch (Exception) 
        {
            Log("✓ ResourceHandle is invalid after release()");
        }
    
        // Check that handle is now owned externally and alive count updated correctly
        int aliveAfterRelease = ResourceHandle.GetAliveCount();
        if (aliveAfterRelease != initialAlive + 1) 
        {
            Log($"✗ TEST 7 FAILED: Alive count mismatch after release. " +
                $"Expected {initialAlive + 1}, got {aliveAfterRelease}");
            return "false";
        }
    
        cross_call_master.cross_call_master.ResourceHandleDestroy(handle);
    
        Log("✓ TEST 7 PASSED: Ownership transfer working correctly\n");
        return "true";
    }
}
