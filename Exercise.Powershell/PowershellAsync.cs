using System;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Powershell
{
    public static class PowershellAsync
    {
        public static async Task<string> RunScript(string scriptText)
        {
            using (PowerShell ps = PowerShell.Create().AddScript(scriptText))
            {
                ps.Streams.Progress.DataAdded += ProgressEventHandler;
                ps.Streams.Information.DataAdded += InfoEventHandler;

                // Create an IAsyncResult object and call the
                // BeginInvoke method to start running the 
                // pipeline asynchronously.
                var runspaceResult = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);

                // Using the PowerShell.EndInvoke method, get the
                // results from the IAsyncResult object.
                StringBuilder stringBuilder = new StringBuilder();

                foreach (PSObject result in runspaceResult)
                {
                    stringBuilder.AppendLine(result.ToString());
                }

                return stringBuilder.ToString();
            }
        }

        static void ProgressEventHandler(object sender, DataAddedEventArgs e)
        {
            ProgressRecord newRecord = ((PSDataCollection<ProgressRecord>)sender)[e.Index];
            if (newRecord.PercentComplete != -1)
            {
                Console.Clear();
                Console.WriteLine("Progress updated: {0}", newRecord.PercentComplete);
            }
        }

        private static void InfoEventHandler(object sender, DataAddedEventArgs e)
        {
            InformationRecord newRecord = ((PSDataCollection<InformationRecord>)sender)[e.Index];
            Console.WriteLine("Info: {0}", newRecord);
        }
    }
}
