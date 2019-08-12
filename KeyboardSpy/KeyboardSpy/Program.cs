using System;
using System.Windows.Forms;


namespace KeyboardSpy
{
    class InterceptKeys
    {
        public static void Main()
        {
            KeyboardSpy.SetAction(action);
            KeyboardSpy.SetHook();
            Application.Run();
            KeyboardSpy.Unhook();
        }

        static void action (int vkCode)
        {
            Console.WriteLine((Keys)vkCode);
        }
    }
}