import-module C:\dev\h1psh\HubOne.PS.dll
show-wfmkeyfetcher
$invoices=get-wfminvoices


$iim1=new-object -ComObject "imacros"
$iim1.iiminit()
$iim1.iimPlayCode("TAB T=1")
$iim1.iimPlayCode("TAB CLOSEALLOTHERS")
$iim1.iimPlayCode("URL GOTO=https://my.workflowmax.com")
Write-Host "Login Correctly and then press any key to continue ..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
$iim1.iimPlayCode("SIZE X=1707 Y=987")


for($i=0; $i -le $invoices.count;$i++)
{
    $url1="https://my.workflowmax.com/financial/invoiceview.aspx?id="
    $url1=$url1 + $invoices[$i].InternalId
    $iim1.iimPlayCode("URL GOTO=" + $url1)
    $iim1.iimPlayCode("TAG POS=1 TYPE=A FORM=NAME:aspnetForm ATTR=TXT:Delete<SP>Invoice")
    $iim1.iimPlayCode("DS CMD=CLICK X=800 Y=520 CONTENT=")
    $iim1.iimPlayCode("WAIT SECONDS=1")

}


# SIG # Begin signature block
# MIIFmAYJKoZIhvcNAQcCoIIFiTCCBYUCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUR8IOeDS0gkzReh2NB6DsHoYN
# pomgggMsMIIDKDCCAhSgAwIBAgIQSJb0DIZqcplPzwrN9pBMtjAJBgUrDgMCHQUA
# MCExHzAdBgNVBAMTFkh1Yk9uZSBQb3dlcnNoZWxsIFJvb3QwHhcNMTMxMTI3MjIw
# NjI1WhcNMzkxMjMxMjM1OTU5WjAaMRgwFgYDVQQDEw9Qb3dlclNoZWxsIFVzZXIw
# ggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC4YhXd1sRC3pki/dclZ4Fp
# r2a2mKlGiPdN0nl9/xsKJWOt73UkjaRiuxRcUZjCxGWS05RKa37/zAN1r0bv+pwI
# WSpntWdL+Mf6n9Gzv+EIALWwGaDzqM793v7NGCL7Be4HWb0QUNdvKgshek0s8WCN
# 86nNVrnyK5IgvT9aaRrTxCwzrxPGaHnbucWcMyVS6zQQFjOfiaVBsQGKi0GlfGN0
# oJXgBu2HNCoDfL08WC2e19ZFCEu0mmYaPHqKGUToe4MZPQbAemeg7+kFmTUNQsS3
# 3zX7oYZ2cAHHLkvcG54wgISTmXvO/JZLlJEYwM2Bb86gzI+XnPmGGP5zZaHsBqK/
# AgMBAAGjazBpMBMGA1UdJQQMMAoGCCsGAQUFBwMDMFIGA1UdAQRLMEmAEO5w9t7M
# m1eQxqK+6N644imhIzAhMR8wHQYDVQQDExZIdWJPbmUgUG93ZXJzaGVsbCBSb290
# ghDWwuEvZOWzrUvoR3irnoKdMAkGBSsOAwIdBQADggEBAByh0C+FO2AtMoJ2SIB2
# lY8vA4ID/ZqsOe0porffpULcF4Gg2NPwCAD4wE86dKdHhCi+8H8i8ep5fI2wg1Pc
# C2hmG592QBs2fXPM+Og3Ha/fr8Dqw5zrThxfVepjvgiEmsCY5K/xgLpYDSpw9HXm
# wzMU24OzsitVhsGxlWpgUDJmNxbRSHWy2p7+4WqVLeBWTWDpnCwVoXdAwpNhMf2p
# kX0u/Y5HVluQ4yvFtoYvsZxV3vmHtFMnpNrDK2LeI9VYBDTR1kM8QNVPmtg6PmgR
# mB74Q5UQ0iRUZAc69Iig389DfcH+Hoq9zINvdXxXHkPXcJ6w1htUtazv/2KuV2gL
# qkgxggHWMIIB0gIBATA1MCExHzAdBgNVBAMTFkh1Yk9uZSBQb3dlcnNoZWxsIFJv
# b3QCEEiW9AyGanKZT88KzfaQTLYwCQYFKw4DAhoFAKB4MBgGCisGAQQBgjcCAQwx
# CjAIoAKAAKECgAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGC
# NwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFEo61cLsdK4FyhJB
# NvG+ZF/xl1/wMA0GCSqGSIb3DQEBAQUABIIBAF59zoOHuYXxIBAA2kvV3LII9EVv
# rlwSfsnCilcjKNsd5nmzhsOSimDn0fbAoyAQUxdClWL84Pfkzxpbr03VTB16TR64
# pFWdKObrf5wY26lbC7ZGVEBofvIufH/JQpm6XyT5VjJfAU1hhfMWJFP9lcB5OgcK
# 9gDw2O7DEgeISO8ZbAxT5qNPdiUAxG/RCAW/aPkElM+GBWPMcK+ajm7oSMOMd+tj
# zHmtHVNGps1y4ckaMpeEI82owaZih2AfWyM2VDUZtNLf6rfhy+tXt8R/UjIPKDIn
# IZgJC5YHgTo8rieLOqremvdAOlhCCKDFIUl2M8Kbag0DnCCkWIgHkIuiKVI=
# SIG # End signature block
