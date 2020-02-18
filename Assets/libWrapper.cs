using System.Runtime.InteropServices;
using UnityEngine;

static public class libWrapper
{
    [DllImport("2020_5A_AL1_CppDllForUnity.dll")]
    public static extern int GiveMe42FromC();

}
