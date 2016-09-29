# reset the lists of hosts prior to looping 
$OutageHosts = $Null 
# specify the time you want email notifications resent for hosts that are down 
$EmailTimeOut = 30 
# specify the time you want to cycle through your host lists. 
$SleepTimeOut = 45 
# specify the maximum hosts that can be down before the script is aborted 
$MaxOutageCount = 10 
# specify who gets notified 
$notificationto = "550700860@qq.com" 
# specify where the notifications come from 
$notificationfrom = "jsyk_YdBan@126.com" 
# specify the SMTP server 
$smtpserver = "smtp.126.com" 
$password = "jizwwslsngwbqbpe" 
#$mycredentials = Get-Credential
Function Send-EMail {
    Param (
        [Parameter(Mandatory=$false)]
        [String]$EmailTo="issumao@126.com,550700860@qq.com,609805657@qq.com",
         
        [Parameter(Mandatory=$false)]
        [String]$Subject,
        [Parameter(Mandatory=$false)]
        [String]$Body,
        [Parameter(Mandatory=$false)]
        [String]$EmailFrom="jsyk_YdBan@126.com",  #This gives a default value to the $EmailFrom command
    
        [Parameter(mandatory=$false)]
        [String]$Password="jizwwslsngwbqbpe"
  )
        Write-Host "emailto"+$EmailTo+$Cc
        $SMTPServer = "smtp.126.com"
        $SMTPMessage = New-Object System.Net.Mail.MailMessage($EmailFrom,$EmailTo,$Subject,$Body)
         
        $SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer) 
        $SMTPClient.EnableSsl = $true 
        $SMTPClient.Credentials = New-Object System.Net.NetworkCredential($EmailFrom.Split("@")[0], $Password); 
        $SMTPClient.Send($SMTPMessage)
        Remove-Variable -Name SMTPClient
        Remove-Variable -Name Password

} #End Function Send-EMail

# start looping here 
Do{ 
$available = $Null 
$notavailable = $Null 
Write-Host (Get-Date) 
 
# Read the File with the Hosts every cycle, this way to can add/remove hosts 
# from the list without touching the script/scheduled task,  
# also hash/comment (#) out any hosts that are going for maintenance or are down. 
get-content  hosts.txt | Where-Object {!($_ -match "#")} |  
ForEach-Object { 
if(Test-Connection -ComputerName $_ -Count 1 -ea silentlycontinue) 
    { 
     # if the Host is available then just write it to the screen 
     write-host "健在的服务器 ---> "$_ -BackgroundColor Green -ForegroundColor White 
     [Array]$available += $_ 
      Send-EMail   -Body "$body" -Subject "  $_ 正常" 
    } 
else 
    { 
     # If the host is unavailable, give a warning to screen 
     write-host "挂掉的服务器 ------------> "$_ -BackgroundColor Magenta -ForegroundColor White 
     if(!(Test-Connection -ComputerName $_ -Count 4 -ea silentlycontinue)) 
       { 
        # If the host is still unavailable for 4 full pings, write error and send email 
        write-host "挂掉的服务器 ------------> "$_ -BackgroundColor Red -ForegroundColor White 
        [Array]$notavailable += $_ 
 
        if ($OutageHosts -ne $Null) 
            { 
                if (!$OutageHosts.ContainsKey($_)) 
                { 
                 # First time down add to the list and send email 
                 Write-Host "$_ 第一次挂掉" 
                 $OutageHosts.Add($_,(get-date)) 
                 $Now = Get-date 
                 $Body = "$_没反应了 $Now" 
                 Send-EMail   -Body "$body" -Subject "  $_ 挂了" 
                } 
                else 
                { 
                    # If the host is in the list do nothing for 1 hour and then remove from the list. 
                    Write-Host "$_ 加入挂掉列表" 
                    if (((Get-Date) - $OutageHosts.Item($_)).TotalMinutes -gt $EmailTimeOut) 
                    {$OutageHosts.Remove($_)} 
                } 
            } 
        else 
            { 
                # First time down create the list and send email 
                
                $OutageHosts = @{$_=(get-date)} 
                $Body = "$_ 挂了 $Now"  
                Send-EMail   -Body "$body" -Subject " $_ 挂了"   
            }  
       } 
    } 
} 
# Report to screen the details 
Write-Host "活着的:"$available.count 
Write-Host "挂掉的:"$notavailable.count 
 
$OutageHosts 
Write-Host "" 
Write-Host "等待 $SleepTimeOut 秒之后再检查................" 
sleep $SleepTimeOut 
if ($OutageHosts.Count -gt $MaxOutageCount) 
{ 
    # If there are more than a certain number of host down in an hour abort the script. 
    $Exit = $True 
    $body = $OutageHosts | Out-String 
    Send-EMail   -Body "$body" -Subject "  $_ 挂了"   
} 
} 
while ($Exit -ne $True) 
 