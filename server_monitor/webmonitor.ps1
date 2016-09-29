##############################################################################  
##  
## Website Availability Monitoring  
## Created by Sravan Kumar S  and modified by Doundis Angelos 
## Date : 25 Apr 2013 | Modification Date 1 Nov 2014 
## Version : 1.0  
 ## Email: sravankumar.s@outlook.com | dangelorisk@hotmail.com 
##############################################################################  
$Global:AmountError=0
$Global:Result = @()  
$Global:Result200=@()
 
Function Test-Websites {  

## The URI list to test  
$URLListFile = "urllist.txt"   
$URLList = Get-Content $URLListFile -ErrorAction SilentlyContinue  

 
  $Timelast = Measure-Command {  
  Foreach($Uri in $URLList) {  
  $time = try{  
  $request = $null  
   ## Request the URI, and measure how long the response took.  
  $result1 = Measure-Command { $request = Invoke-WebRequest -Uri $uri }  
  $result1.TotalSeconds  
  }   
  catch  
  {  
   <# If the request generated an exception (i.e.: 500 server  
   error or 404 not found), we can pull the status code from the  
   Exception.Response property #>  
   $request = $_.Exception.Response  
   $time = -1  
  } 
 
  $r = [int] $request.StatusCode 
   $response= [PSCustomObject] @{  
          Time = Get-Date;  
          Uri = $uri;  
          StatusCode = [int] $request.StatusCode;  
          StatusDescription = $request.StatusDescription;  
          ResponseLength = $request.RawContentLength;  
          TimeTaken =  $time;}
   if($r -ne "200")
   {
   $result +=$response
   $Global:AmountError+=1
   
   }
   else
   {
   $Result200+=$response
  
   }
   
  } 
}  
$ExecTime = $timelast.TotalSeconds 
#Prepare email body in HTML format for Unsuccess table 
if($result -ne $null)  
{  
    $Outputreport = "<div><H2>[Website(s) Unavailable]</H2><Table border=1 cellpadding=5 cellspacing=0><TR bgcolor=gray align=center><TD><B>URL</B></TD><TD><b>Link</b></TD><TD><B>Status Code</B></TD><TD><B>Status</B></TD><TD><B>Time Taken</B></TD><TD><B>Response Length</B></TD><TD><B>Timestamp</B></TD</TR>"  
    Foreach($Entry in $Result)  
    {  
    switch ($Entry.StatusCode) 
        { 
        "404" {$Outputreport += "<TR bgcolor='#FF704D' style='color:#fff;'>"} 
        "0" {$Outputreport += "<TR bgcolor='#FF704D' style='color:#fff;'>"} 
        default {"<TR>"} 
        } 
        $Outputreport += "<TD>$($Entry.uri)</TD><TD style='text-align:center;'><a href='$($Entry.uri)' target='_blank'> Go </a></TD><TD align=center>$($Entry.StatusCode)</TD><TD align=center>$($Entry.StatusDescription)</TD><TD align=center>$($Entry.TimeTaken)</TD><TD align=center>$($Entry.ResponseLength)</TD><TD align=center>$($Entry.Time)</TD></TR>"  
    }  
    $Outputreport += "</Table></div>"  
}  
<#
 
#>
 
if($result200 -ne $null)  
{  
    $Outputreport200 = "<div><H2 style='color:green;'>[Live Website's]</H2><Table border=1 cellpadding=5 cellspacing=0><TR bgcolor=gray align=center><TD><B>URL</B></TD><TD><b>Link</b></TD><TD><B>Status Code</B></TD><TD><B>Status</B></TD><TD><B>Time Taken</B></TD><TD><B>Response Length</B></TD><TD><B>Timestamp</B></TD</TR>"  
    Foreach($Entry in $Result200)  
    {  
    switch ($Entry.StatusCode) 
        { 
        "200" {$Outputreport200 += "<TR bgcolor='#D6FFFF' style='color:#000;'>"} 
        default {"<TR>"} 
        } 
        $Outputreport200 += "<TD>$($Entry.uri)</TD><TD style='text-align:center;'><a href='$($Entry.uri)' target='_blank'> Go </a></TD><TD align=center>$($Entry.StatusCode)</TD><TD align=center>$($Entry.StatusDescription)</TD><TD align=center>$($Entry.TimeTaken)</TD><TD align=center>$($Entry.ResponseLength)</TD><TD align=center>$($Entry.Time)</TD></TR>"  
    }  
    $Outputreport200 += "</Table></div>"  
}  
 
 
$Results = "<div>In total <b>" + $URLList.count + " </b> websites where tested in " + $ExecTime + " seconds, see results below:</div>" 
$Results += "<TITLE style='color:red;'>[Errors] Website Availability Report</TITLE><BODY background-color:peachpuff> $Outputreport  $Outputreport200  </BODY>" 
$Date = (Get-Date -format "dd-MM-yyyy_hh-mm-ss")
 
$OutputFile = "$PSScriptRoot\WebcheckResults_$Date.html"
#$Results | out-file $OutputFile 
#Invoke-Expression $OutputFile 
return $Results 
} 
Function Send-mail { 

    #Send email with atachment 
    $EmailFrom = "jsyk_YdBan@126.com" 
    $EmailTo = "issumao@126.com,550700860@qq.com,609805657@qq.com" 
    $SMTPServer = "smtp.126.com" 
    $EmailSubject = "ydb服务异常"  
 
    #Send mail with output 
    $mailmessage = New-Object system.net.mail.mailmessage  
    $mailmessage.from = ($EmailFrom)  
    $mailmessage.To.add($EmailTo) 
    $mailmessage.Subject = $EmailSubject 
    $mailmessage.Body = $body 
    $mailmessage.IsBodyHTML = $true 
    $SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer)   
    $SMTPClient.Credentials = New-Object System.Net.NetworkCredential("jsyk_YdBan@126.com", "jizwwslsngwbqbpe");  
    #$SMTPClient.EnableSsl = $true  
    $SMTPClient.Send($mailmessage) 
 
} 

DO{ 
$Global:AmountError=0
$body = tEst-Websites 
 
Write-Host "服务不正常数量:$Global:AmountError"

if ($Global:AmountError -gt 0) { 
Write-Host "开始发送邮件"
Send-mail($body)
}
Write-Host("30秒后再检查")
sleep 30
}
while ($Exit -ne $True) 