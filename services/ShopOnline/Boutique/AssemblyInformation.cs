using System;
using System.Reflection;

namespace Boutique
{
    public class AssemblyInformation
    {
        public static Assembly Assembly { get; } = Assembly.GetExecutingAssembly(); 
    }
}
