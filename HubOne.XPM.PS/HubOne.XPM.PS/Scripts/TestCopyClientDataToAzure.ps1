# Copy-ClientDataToAzure
# This script will copy data from a mapped network drive up to Azure.
# 1. Get Azure credentials or pass from C#
# 2. Generate log of entire original client file structure
# 3. Generate mapping file according to SQL source of client data (RedMap, MYOB...) for WorkflowMax
# 4. Retrieve Azure Storage Account / Create if does not exist
# 5. Retrieve Azure Storage Container / Create if does not exist
# 6. Copy entire client data source as is to Azure storage using AzCopy

Copy-ClientDataToAzure 'Dobbin and Associates' 'C:\\CLIENT SERVER' 'C:\\DATA MIGRATION' 'HubOne' 'fulllarry' 'false' 'MYOB' 'false'

function Copy-ClientDataToAzure {
      param([Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientName,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientSourceDirectory,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$logFilesDirectory,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azSubscriptionName,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azStorageName,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$isDeltaCopy,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$mapFileDataSource,
            [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$startFolderWatch)

#region Azure Stuff
#region global variables
$global:createStorageAccountResult = $null
$global:createContainerResult = $null
$global:azStorageAccount = $null
$global:azStorageAccountKey = $null
$global:azStorageContainer = $null

$global:azContext = $null
$global:copyToAzureResult = $null
$azStorageName = $azStorageName.ToLower()
 
$date = $(Get-Date -Format HHmmmssddMMyyy)
$azContainerName = 'clientsource'
$formattedClientName = $clientName -replace ' ', ''
$sourceFileLog = $logFilesDirectory + '\\ClientToAzure_OriginalSourceLog_' + $formattedClientName + '_' + $date + '.csv'
$dataCopyLog = $logFilesDirectory + '\\ClientToAzure_DataCopyLog_' + $formattedClientName + '_' + $date + '.log'
$global:processLogFile = $logFilesDirectory + '\\ClientToAzure_ProcessLog_'  + $formattedClientName + '_' + $date + '.log'
#endregion

$LogStartDT = "Log started on {0}" -f $date
_LogWrite ("STARTING------------------------------------------------------") 
_LogWrite ("$LogStartDT") 

#region get credentials and write output
$Cred 

#only get credneital if not supplied from external source
if($Cred -eq $null)
{
    $Cred = Get-Credential
    if($Cred.Password -eq $null){
        _LogWrite ('User credentials not valid')
        exit
    }
}

_LogWrite ('Operating user is ' + $Cred.UserName)

$subscription = Select-AzureSubscription -SubscriptionName $azSubscriptionName
_LogWrite ("VARIABLES----------------------------------------------------") 
_LogWrite ('Azure Subscription ID is ' + $subscription.ID)
_LogWrite ('Formatted ' + $formattedClientName)
_LogWrite ('Client Source ' + $clientSourceDirectory)
_LogWrite ('Log Directory ' + $logFilesDirectory)
#endregion

_Log-InitialClientSourceFileStructure $clientSourceDirectory $logFilesDirectory

#region get or create new azure storage account
$global:azStorageAccount = Get-AzureStorageAccount $azStorageName
if($global:azStorageAccount -eq $null){
    _Create-AzureStorageAccount $azSubscriptionName $azStorageName $azContainerName $Cred
    if($global:createStorageAccountResult -ne $null){
        $global:azStorageAccount = Get-AzureStorageAccount $azStorageName
    }
}
#endregion

#region get or create new azure container
$global:azStorageAccountKey = Get-AzureStorageKey $azStorageName | %{$_.Primary}
$global:azContext = New-AzureStorageContext -StorageAccountName $azStorageName -StorageAccountKey $azStorageAccountKey
    
$global:azStorageContainer = Get-AzureStorageContainer -Context $global:azContext -Name $azContainerName
if($global:azStorageContainer -eq $null){
    _Create-AzureStorageContainer
    if($global:createContainerResult -ne $null){
        $global:azStorageContainer = Get-AzureStorageContainer -Context $global:azContext -Name $azContainerName
    } 
}
#endregion

#endregion

_Initiate-DataTransfer $azStorageName $azContainerName $clientSourceDirectory $dataCopyLog

_Update-BlobProperties $azContainerName

}

function _Update-BlobProperties([string]$azContainerName)
{
     _LogProcess ("Getting Azure Storage Blobs")
    $blobs = Get-AzureStorageBlob -Container $azContainerName -Context $global:azContext
    _LogProcess ('Retrieved {0} blobs from Azure storage' -f $blobs.Length)

    #Load array of transport objects - one for each file to be transported to SharePoint
    $arrayOfTransportObjects = @()
    foreach($blob in $blobs)
    {
        $properties = @{"HubOneOriginalLastModified" = 'public, max-age=86400'}
        Set-AzureStorageBlobContent -Blob $blob.Name -Container $azContainerName -Context $global:azContext -Properties $properties
    }
}

function _Log-InitialClientSourceFileStructure([string]$clientSourceDirectory, [string]$logDirectory){
    
    _LogWrite ('Logging client source contents from ' + $clientSourceDirectory)

    $clientSourceItemCount = (Get-ChildItem $clientSourceDirectory -Recurse).Count
    _LogWrite ('Client Source Item Count ' + $clientSourceItemCount)
    Get-ChildItem -Path $clientSourceDirectory -Recurse |`
    foreach{
    $Item = $_
    $Type = $_.Extension
    $Path = $_.FullName
    $Folder = $_.PSIsContainer
    $Created = $_.CreationTime
    $Modified = $_.LastWriteTime

    $Path | Select-Object `
     @{n='Name';e={$Item}},`
     @{n='Created';e={$Created}},`
     @{n='Modified';e={$Modified}},`
     @{n='FilePath';e={$Path}},`
     @{n='Extension';e={if($Folder){'Folder'}else{$Type}}}`
    }| Export-Csv $sourceFileLog -NoTypeInformation 
}

function _Create-AzureStorageAccount([string]$azSubscriptionName, [string]$azStorageName, [string]$azureContainerName, [System.Management.Automation.PSCredential] $cred){
    _LogWrite ('Creating Azure Storage Account [' + $azStorageName + '] on subscription ' + $azSubscriptionName)
    $global:createStorageAccountResult = New-AzureStorageAccount -StorageAccountName $azStorageName -Location 'Southeast Asia'
    if($global:createStorageAccountResult -ne $null)
    {
        _LogWrite ('Azure Storage created successfully!')
    }
}

function _Create-AzureStorageContainer(){
   _LogWrite ('Creating Azure Storage Container')
   $global:createContainerResult = New-AzureStorageContainer -Name $global:azContainerName -Context $global:azContext
    if($global:createContainerResult -ne $null)
    {
        _LogWrite ('Azure Container created successfully!')
    }
}

function _Initiate-DataTransfer([string]$azStorageName, [string]$azContainerName, [string]$clientSourceDirectory, [string]$dataCopyLog){
    $azDestURL = ('https://' + $azStorageName + '.blob.core.windows.net/' + $azContainerName)
    _LogWrite ('Copying client source data to Azure at ' + $azDestURL)
    $azCopy = 'C:\\Program Files (x86)\\Microsoft SDKs\\Azure\\AzCopy\\AzCopy.exe'
    $azPath = 'C:\\Program Files (x86)\\Microsoft SDKs\\Azure\\AzCopy'
    Set-Location $azPath
    $global:copyToAzureResult = .\\AzCopy.exe $clientSourceDirectory $azDestURL /BlobType:block /destkey:$global:azStorageAccountKey  /S /Y /XO /V:$dataCopyLog
}

function _Get-WorkflowMaxClients([string] $wfmxKey){
    _LogWrite ('Getting WorkflowMax Clients')
    $URI1 = "http://modernpractice2014.cloudapp.net/ClientService.svc?wsdl" 
    $wfmxKey = $wfmxKey
    $proxy = New-WebServiceProxy -Uri $URI1 -namespace WebServiceProxy  -UseDefaultCredential
    $resp = $proxy.GetAllClients($wfmxKey, $false, $false)
}

function _Get-SQLMappingFile([string] $dataSourceType){
    _LogWrite ('Getting Mapping File from SQL')
    $URI1 = "http://modernpractice2014.cloudapp.net/ClientService.svc?wsdl" 
    $wfmxKey = $wfmxKey
    $proxy = New-WebServiceProxy -Uri $URI1 -namespace WebServiceProxy  -UseDefaultCredential
    $resp = $proxy.GetAllClients($wfmxKey, $false, $false)
}

function _LogWrite
{ 
   Param ([string]$logstring) 
   $logstring = (get-date).ToString() + ' -------------- ' + $logstring
   Add-content -Path $global:processLogFile -value $logstring
   Write-Output ($logstring)
} 



