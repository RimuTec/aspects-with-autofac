#requires -PSEdition Core
# Additional steps to initialize the development container

# Install sqlserver module so we can use 'Invoke-Sqlcmd'
Install-Module sqlserver -Confirm:$False -Force

# Create new database
# Source for the retry logic:
# https://stackoverflow.com/a/47712807/411428
$attempts=20
$sleepInSeconds=3
do
{
    try
    {
        Invoke-Sqlcmd -ServerInstance "database,1433" -Username sa -Password PassWord42 -Query "CREATE DATABASE [fooservice]";
        Write-Host "Database fooservice created successfully."
        break;
    }
    catch [Exception]
    {
        Write-Host "Retrying..."
    }            
    $attempts--
    if ($attempts -gt 0) { Start-Sleep $sleepInSeconds }
} while ($attempts -gt 0)
