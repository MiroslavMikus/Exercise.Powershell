using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Threading.Tasks;

namespace Exercise.Powershell
{
    class Program
    {

        const string TestProgress = @"
function Test-Progress
{
    param()

    Write-Progress -Activity 'Testing progress' -Status 'Starting' -PercentComplete 0
    Start-Sleep -Milliseconds 600
    1..10 |ForEach-Object{
        Write-Progress -Activity 'Testing progress' -Status 'Progressing' -PercentComplete $(5 + 6.87 * $_)
        Start-Sleep -Milliseconds 400
    }
    Write-Progress -Activity 'Testing progress' -Status 'Ending' -PercentComplete 99
    Start-Sleep -Seconds 2
    Write-Progress -Activity 'Testing progress' -Status 'Done' -Completed
}

Test-Progress
'Done'
";

        const string TestScrpit = @"
Get-Process | Out-string
'process is done'
netstat -ano
";
        static async Task Main(string[] args)
        {
            var result = await PowershellAsync.RunScript(TestScrpit);

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
