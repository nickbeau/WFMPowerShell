using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HubOne.PS.Classes
{
    /// <summary>
    /// Class with helper type methods
    /// </summary>
    public sealed class Helper
    {

        /// <summary>
        /// Get a script from the project scripts folder
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        public static string GetScriptString(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(scriptName))
            if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
            return null;
        }


        /// <summary>
        /// Run the powershell script
        /// </summary>
        /// <param name="psScriptPath"></param>
        /// <returns></returns>
        public static Collection<PSObject> RunPsScript(string psScriptPath)
        {
            string psScript = string.Empty;
            if (File.Exists(psScriptPath))
                psScript = File.ReadAllText(psScriptPath);
            else
                throw new FileNotFoundException("Wrong path for the script file");

            using (Runspace runSpace = RunspaceFactory.CreateRunspace())
            {
                runSpace.Open();
                var runSpaceInvoker = new RunspaceInvoke(runSpace);
                runSpaceInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runSpace;

                ps.AddScript(psScriptPath);
                ps.Invoke();

                ps.AddCommand("Copy-ClientDataToAzure").AddParameters(new Dictionary<string, string>()
                 {
                     { "clientName", "Test" },
                     { "clientSourceDirectory", "true" },
                     { "logFilesLocation", @"Evaluation\EvaluationFormPartialListCV" },
                     { "azureStorageName", @"Evaluation\EvaluationFormPartialListCV" },
                     { "createNewAzureStorage", @"Evaluation\EvaluationFormPartialListCV" },
                     { "copyLogFileLocation", @"Evaluation\EvaluationFormPartialListCV" }
                 });

                foreach (PSObject result in ps.Invoke())
                {
                    Debug.WriteLine("Object : " + result);
                }
            }
            return null;
        }

        /// <summary>
        /// Get script
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetCopyClientSourceScriptString()
        {
            var script = new StringBuilder();
            script.AppendLine("function Copy-ClientDataToAzure {");
            script.AppendLine("param([Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientName,");
            script.AppendLine("[Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientSourceDirectory,");
            script.AppendLine("[Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$logFilesDirectory,");
            script.AppendLine("[Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azureSubscriptionName,");
            script.AppendLine("[Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azureStorageName,");
            script.AppendLine("[Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$createNewAzureStorage)");
            script.AppendLine("$Cred");
            script.AppendLine("$createStorageAccountResult");
            script.AppendLine("$createContainerResult");
            script.AppendLine("$azureStorageAccount");
            script.AppendLine("Write-Output ('User is ' + $Cred.UserName)");
            script.AppendLine("$subscription = Select-AzureSubscription -SubscriptionName $azureSubscriptionName");
            script.AppendLine("Write-Output ('Azure Subscription ID is ' + $subscription.ID)");
            script.AppendLine(" ");
            script.AppendLine("$date = $(Get-Date -Format HHmmmssddMMyyy)");
            script.AppendLine("$azContainerName = 'clientsource'");
            script.AppendLine("$formattedClientName = $clientName -replace ' ', ''");
            script.AppendLine("$sourceFileLog = $logFilesDirectory + '\\ClientSourceLog_' + $formattedClientName + '_' + $date + '.csv'");
            script.AppendLine(" ");
            script.AppendLine("Write-Output ('Formatted ' + $formattedClientName)");
            script.AppendLine("Write-Output ('Client Source ' + $clientSourceDirectory)");
            script.AppendLine("Write-Output ('Log Directory ' + $logFilesDirectory)");
            script.AppendLine(" ");
            
            script.AppendLine("Log-InitialClientSourceFileStructure $clientSourceDirectory $logFilesDirectory");
            script.AppendLine(" ");
            script.AppendLine("if($createNewAzureStorage -e $true){");
            script.AppendLine("   Create-AzureStorageAccount $azureSubscriptionName $azureStorageName $azContainerName $Cred");
            script.AppendLine("   if($createStorageAccountResult -ne $null){");

            script.AppendLine("}");

            script.AppendLine(" if($createContainerResult -ne $null) {");
            script.AppendLine("     Copy-ClientDataToAzure");
            script.AppendLine("}");
            script.AppendLine(" ");
            script.AppendLine(" ");
            script.AppendLine(" ");
            script.AppendLine(" ");
            script.AppendLine(" ");
            script.AppendLine(" ");
          
            script.AppendLine("}");

            script.AppendLine(LogInitialClientSourceMethod());
            script.AppendLine(CreateAzureStorageAccountMethod());
            script.AppendLine(CopyClientDataToAzureMethod());


            return script.ToString();
        }

        private static string LogInitialClientSourceMethod()
        {
            var script = new StringBuilder();
            script.AppendLine("function Log-InitialClientSourceFileStructure([string]$clientSourceDirectory, [string]$logDirectory){");
            script.AppendLine("Write-Output ('Logging client source contents from ' + $clientSourceDirectory)");
            script.AppendLine("$clientSourceItemCount = (Get-ChildItem $clientSourceDirectory -Recurse).Count");
            script.AppendLine("Write-Output ('Client Source Item Count ' + $clientSourceItemCount)");
            script.AppendLine("Get-ChildItem -Path $clientSourceDirectory -Recurse |`");
            script.AppendLine("foreach{");
            script.AppendLine("$Item = $_");
            script.AppendLine("$Type = $_.Extension");
            script.AppendLine("$Path = $_.FullName");
            script.AppendLine("$Folder = $_.PSIsContainer");
            script.AppendLine("$Created = $_.CreationTime");
            script.AppendLine("$Modified = $_.LastWriteTime");
            script.AppendLine("");
            script.AppendLine("$Path | Select-Object `");
            script.AppendLine(" @{n='Name';e={$Item}},`");
            script.AppendLine(" @{n='Created';e={$Created}},`");
            script.AppendLine(" @{n='Modified';e={$Modified}},`");
            script.AppendLine(" @{n='FilePath';e={$Path}},`");
            script.AppendLine(" @{n='Extension';e={if($Folder){'Folder'}else{$Type}}}`");
            script.AppendLine("}| Export-Csv $sourceFileLog -NoTypeInformation ");
            script.AppendLine("}");

            return script.ToString();
        }

        private static string CreateAzureStorageAccountMethod()
        {
            var script = new StringBuilder();
            script.AppendLine("function Create-AzureStorageAccount([string]$azureSubscriptionName, [string]$azureStorageName, [string]$azureContainerName, [System.Management.Automation.PSCredential] $cred){");
            script.AppendLine("Write-Output ('Creating Azure Storage Account [' + $azureStorageName + '] on subscription ' + $azureSubscriptionName)");
            script.AppendLine("$createStorageAccountResult = New-AzureStorageAccount -StorageAccountName $azureStorageName -Location 'Southeast Asia'");
            script.AppendLine("if($createStorageAccountResult -ne $null){");
            script.AppendLine(" $azStorageAccountKey = get-azurestoragekey $azureStorageName | %{$_.Primary}");
            script.AppendLine(" $azContext = New-AzureStorageContext -StorageAccountName $azureStorageName -StorageAccountKey $azStorageAccountKey");
            script.AppendLine(" $createContainerResult = New-AzureStorageContainer -Name $azContainerName -Context $azContext");
            script.AppendLine("  if($createContainerResult -ne $null){");
            script.AppendLine("   Write-Output ('Azure Storage created successfully!')");
            script.AppendLine("  }");
            script.AppendLine(" }");
            script.AppendLine("}");

            return script.ToString();
        }

        private static string CopyClientDataToAzureMethod()
        {
            var script = new StringBuilder();
            script.AppendLine("function Copy-ClientDataToAzure([string]$azureStorageName, [string]$azContainerName, [string]$clientSourceDirectory, [string]$azStorageAccountKey){");
            script.AppendLine("Write-Output ('Copying client source data to Azure...')");
            script.AppendLine("$azCopy = 'C:\\Program Files (x86)\\Microsoft SDKs\\Azure\\AzCopy\\AzCopy.exe'");
            script.AppendLine("$azPath = 'C:\\Program Files (x86)\\Microsoft SDKs\\Azure\\AzCopy'");
            script.AppendLine("Set-Location $azPath");
            script.AppendLine("$azDestURL = 'https://$azureStorageName.blob.core.windows.net/$azContainerName'");
            script.AppendLine("Write-Output ('Destination URL ' + $azDestURL)");
            script.AppendLine("$copyResult = .\\AzCopy.exe $clientSourceDirectory $azDestURL /BlobType:block /destkey:$azStorageAccountKey /S /Y /XO /V:$CopyLogFile");
            script.AppendLine(" ");

            script.AppendLine("}");

            return script.ToString();
        }
    }
}
