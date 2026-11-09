$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$guiProject = Join-Path $root 'src\SmoothScrollGUI\SmoothScrollGUI.csproj'
$guiOutput = Join-Path $root 'src\SmoothScrollGUI\bin\Release\net48'
$appDir = Join-Path $root 'app'
$setupProject = Join-Path $root 'src\SmoothScrollSetup\SmoothScrollSetup.csproj'
$payload = Join-Path $root 'src\SmoothScrollSetup\payload.zip'
$setupOutput = Join-Path $root 'src\SmoothScrollSetup\bin\Release\net48\SmoothScrollSetup.exe'
$distDir = Join-Path $root 'dist'
$finalSetup = Join-Path $distDir 'SmoothScrollSetup.exe'
$checksumFile = Join-Path $distDir 'SmoothScrollSetup.exe.sha256'
$sumsFile = Join-Path $root 'SHA256SUMS.txt'

Add-Type -AssemblyName System.IO.Compression.FileSystem

Write-Host 'Building SmoothScrollGUI...'
dotnet build $guiProject -c Release
if ($LASTEXITCODE -ne 0) {
    throw "GUI build failed with exit code $LASTEXITCODE"
}

Write-Host 'Updating app payload...'
Copy-Item -LiteralPath (Join-Path $guiOutput 'SmoothScrollGUI.exe') -Destination $appDir -Force
Copy-Item -LiteralPath (Join-Path $guiOutput 'SmoothScrollGUI.exe.config') -Destination $appDir -Force

if (Test-Path -LiteralPath $payload) {
    Remove-Item -LiteralPath $payload -Force
}

[System.IO.Compression.ZipFile]::CreateFromDirectory(
    $appDir,
    $payload,
    [System.IO.Compression.CompressionLevel]::Optimal,
    $false
)

Write-Host 'Building SmoothScrollSetup...'
dotnet build $setupProject -c Release
if ($LASTEXITCODE -ne 0) {
    throw "Installer build failed with exit code $LASTEXITCODE"
}

New-Item -ItemType Directory -Force -Path $distDir | Out-Null
Copy-Item -LiteralPath $setupOutput -Destination $finalSetup -Force

$file = Get-Item -LiteralPath $finalSetup
$hash = (Get-FileHash -LiteralPath $finalSetup -Algorithm SHA256).Hash.ToLower()

"$hash *$($file.Name)" | Set-Content -LiteralPath $checksumFile -Encoding ascii

$sums = @(
    '# Scrolling Smoother shipped binaries',
    '# Format: SHA256  relative/path',
    '',
    "$hash  dist/SmoothScrollSetup.exe"
)

Get-ChildItem -LiteralPath $appDir -File | Sort-Object Name | ForEach-Object {
    $appHash = (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash.ToLower()
    $sums += "$appHash  app/$($_.Name)"
}

$sums | Set-Content -LiteralPath $sumsFile -Encoding ascii

Write-Host ''
Write-Host "Installer: $($file.FullName)"
Write-Host "Size     : $($file.Length) bytes"
Write-Host "SHA256   : $hash"
Write-Host "Checksum : $checksumFile"
Write-Host "Sums     : $sumsFile"
