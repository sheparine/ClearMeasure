# bypass execution policy for this scope
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

# get script directory
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Write-Host "My directory is $dir"

# cd to script directory
Push-Location $dir
#run all tests
dotnet test
# return to original directory
Pop-Location

Pause