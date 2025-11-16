using Plugify;
using System.Numerics;

namespace ExamplePlugin
{
    public class ExamplePlugin : Plugin
    {
        public void OnStart()
        {
            Console.WriteLine(".NET: OnStart");
        }

        public void OnEnd()
        {
            Console.WriteLine(".NET: OnEnd");
        }
    }

    /// <summary>
    /// Example exported functions to demonstrate the Plugify manifest generator
    /// </summary>
    public static class ExportedFunctions
    {
        /// <summary>
        /// Simple function that adds two integers
        /// </summary>
        [NativeExport("AddNumbers")]
        public static int AddNumbers_Exported(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Function that processes an array of doubles
        /// </summary>
        [NativeExport("ProcessData")]
        public static string[] ProcessData_Exported(double[] data, string prefix)
        {
            return data.Select(value => $"{prefix}{value}").ToArray();
        }

        /// <summary>
        /// Function with ref parameter
        /// </summary>
        [NativeExport("ModifyValue")]
        public static void ModifyValue_Exported(ref int value)
        {
            value *= 2;
        }

        /// <summary>
        /// Function that works with Vector3
        /// </summary>
        [NativeExport("CalculateDistance")]
        public static float CalculateDistance_Exported(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        /// <summary>
        /// Function with multiple parameter types
        /// </summary>
        [NativeExport("ComplexFunction")]
        public static string ComplexFunction_Exported(
            int count,
            string name,
            float[] values,
            bool enabled)
        {
            return $"Count: {count}, Name: {name}, Values: {values.Length}, Enabled: {enabled}";
        }

        /// <summary>
        /// Function with callback delegate
        /// </summary>
        [NativeExport("ExecuteWithCallback")]
        public static void ExecuteWithCallback_Exported(int value, string inputStr, ExampleCallback callback)
        {
            string result = callback(value, inputStr);
            Console.WriteLine($"Callback result: {result}");
        }

        /// <summary>
        /// Function that returns an array
        /// </summary>
        [NativeExport("GetRange")]
        public static int[] GetRange_Exported(int start, int count)
        {
            return Enumerable.Range(start, count).ToArray();
        }

        /// <summary>
        /// Function with all basic types
        /// </summary>
        [NativeExport("TestAllTypes")]
        public static void TestAllTypes_Exported(
            sbyte int8Val,
            short int16Val,
            int int32Val,
            long int64Val,
            byte uint8Val,
            ushort uint16Val,
            uint uint32Val,
            ulong uint64Val,
            float floatVal,
            double doubleVal,
            string stringVal,
            bool boolVal)
        {
            Console.WriteLine($"Received all types: {int8Val}, {int16Val}, {int32Val}, {int64Val}, " +
                            $"{uint8Val}, {uint16Val}, {uint32Val}, {uint64Val}, " +
                            $"{floatVal}, {doubleVal}, {stringVal}, {boolVal}");
        }
    }

    /// <summary>
    /// Example callback delegate
    /// </summary>
    public delegate string ExampleCallback(int a, string b);
}