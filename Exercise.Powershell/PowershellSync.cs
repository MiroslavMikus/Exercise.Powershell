using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Exercise.Powershell
{
    public static class PowershellSync
    {
        public static Collection<PSObject> Run(string scriptText)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                return ps.AddScript(scriptText).Invoke();
            }
        }
    }
}
