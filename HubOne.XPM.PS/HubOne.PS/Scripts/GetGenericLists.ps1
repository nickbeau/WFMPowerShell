Import-Module "E:\OnePractice\OnePracticeApps\HubOne.PS\bin\Debug\HubOne.PS.dll"

$siteURL = "https://hubone.sharepoint.com/sites/modernpractice/testdoc"
$userName = "laurence@hubone.com"
$passWord = "Hootsie656565$"

Get-GenericSpList -SharePointUrl "https://hubone.sharepoint.com/sites/modernpractice/testdoc" `
-UserName "laurence@hubone.com" -Password "Hootsie656565$" -ListName "Staff" -OrderBy "StaffName" -IncludeRelationships $true