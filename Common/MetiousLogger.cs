using System;
namespace Common
{
    public class MetiousLogger
    {
        public static void Message(string modName, string message)
        {
            Console.WriteLine(modName + "" + message);
        }
        public static void GenericError(string modName, Exception e)
        {
            Console.WriteLine(modName + "ERROR: " + e);
        }
        public static void PatchStart(string modName, string version)
        {
            Console.WriteLine(modName + "Started Patching, Version: " + version);
        }
        public static void PatchComplete(string modName)
        {
            Console.WriteLine(modName + "Patching Completed!");
        }
        public static void PatchFailed(string modName, Exception e)
        {
            Console.WriteLine(modName + "Patching Failed, Exception: " + e);
        }
    }
}
