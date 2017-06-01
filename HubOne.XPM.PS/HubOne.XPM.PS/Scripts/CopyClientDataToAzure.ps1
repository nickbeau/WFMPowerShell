function Copy-ClientDataToAzure {
  param([Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientName,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$clientSourceDirectory,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$logFilesDirectory,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azureSubscriptionName,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$azureStorageName,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$createNewAzureStorage,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$isDeltaCopy,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$createMapFile,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$mapFileDataSource,
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)]$startFolderWatch
        )

$Cred
Select-AzureSubscription -SubscriptionName HubOne

$AzCopy = "C:\Program Files (x86)\Microsoft SDKs\Azure\AzCopy\AzCopy.exe"
$AzPath = "C:\Program Files (x86)\Microsoft SDKs\Azure\AzCopy"
Set-Location $AzPath

$AzContainerName = "clientsource"
[string]$CopyLogFile = "C:\DATA MIGRATION\AzDataCopyLog$Date.log"

Log-InitialClientSourceFileStructure

$CreateStorageAccountResult = New-AzureStorageAccount -StorageAccountName $azureStorageName -Location "Southeast Asia"
if($CreateStorageAccountResult -ne $null)
{
    $AzStorageAccountKey = get-azurestoragekey $azureStorageName | %{$_.Primary}
    $AzContext = New-AzureStorageContext -StorageAccountName $azureStorageName -StorageAccountKey $AzStorageAccountKey

    $CreateContainerResult = New-AzureStorageContainer -Name $AzContainerName -Context $AzContext
    if($CreateContainerResult -ne $null)
    {
        $AzDestURL = "https://$azureStorageName.blob.core.windows.net/$AzContainerName"
        $CopyResult = .\AzCopy.exe $clientSourceDirectory $AzDestURL /BlobType:block /destkey:$AzStorageAccountKey /S /Y /XO /V:$CopyLogFile
        $CopyResult
    }
}

Write-Object "ALO"

}


Function Log-InitialClientSourceFileStructure
{
    Write-Host "Logging client source contents"

    $ClientSourceItemCount = (Get-ChildItem $ClientSourceFolder -Recurse).Count
    Get-ChildItem -Path $ClientSourceFolder -Recurse |`
    foreach{
    $Item = $_
    $Type = $_.Extension
    $Path = $_.FullName
    $Folder = $_.PSIsContainer
    $Created = $_.CreationTime
    $Modified = $_.LastWriteTime

    $Path | Select-Object `
        @{n="Name";e={$Item}},`
        @{n="Created";e={$Created}},`
        @{n="Modified";e={$Modified}},`
        @{n="FilePath";e={$Path}},`
        @{n="Extension";e={if($Folder){"Folder"}else{$Type}}}`
        }| Export-Csv $ClientSourceLogLocation -NoTypeInformation 

}

Function Log-DeltaClientSourceFileStructure
{
    Write-Host "Logging client source contents"

    $ClientSourceItemCount = (Get-ChildItem $ClientSourceFolder -Recurse).Count
    Get-ChildItem -Path $ClientSourceFolder -Recurse |`
    foreach{
    $Item = $_
    $Type = $_.Extension
    $Path = $_.FullName
    $Folder = $_.PSIsContainer
    $Created = $_.CreationTime
    $Modified = $_.LastWriteTime

    $Path | Select-Object `
        @{n="Name";e={$Item}},`
        @{n="Created";e={$Created}},`
        @{n="Modified";e={$Modified}},`
        @{n="FilePath";e={$Path}},`
        @{n="Extension";e={if($Folder){"Folder"}else{$Type}}}`
        }| Export-Csv $ClientSourceLogLocation -NoTypeInformation 

}


get-host
