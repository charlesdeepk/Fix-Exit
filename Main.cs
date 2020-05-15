using MelonLoader;
using System;
using UnityEngine;

namespace FixExit
{
    public class FixExitMod : MelonMod
    {
        private static void Hook(IntPtr target, IntPtr detour)
        {
            typeof(Imports).GetMethod("Hook", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).Invoke(null, new object[] { target, detour });
        }

        public override void OnApplicationStart()
        {
            IntPtr funcToHook = (IntPtr)typeof(Application).GetField("NativeMethodInfoPtr_Quit_Public_Static_Void_0", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
            Hook(funcToHook, new System.Action<IntPtr>((@this) =>
            {
                MelonModLogger.Log(ConsoleColor.Cyan, "Goodbye!");
                System.Environment.Exit(0);
            }).Method.MethodHandle.GetFunctionPointer());
        }
    }
}
