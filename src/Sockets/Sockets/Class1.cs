using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Module Initializers

class Class1
{
    [ModuleInitializer]
    internal static void Class11()
    {
        Console.WriteLine("Class11 Run...");
    }

    [ModuleInitializer]
    internal static void Class12()
    {
        Console.WriteLine("Class12 Run...");
    }
}
