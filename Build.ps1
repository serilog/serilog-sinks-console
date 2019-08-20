Write-Host "build: Build started"

Push-Location $PSScriptRoot

if(Test-Path .\artifacts) {
    Write-Host "build: Cleaning .\artifacts"
	Remove-Item .\artifacts -Force -Recurse
}

& dotnet restore --no-cache

$branch = @{ $true = $env:APPVEYOR_REPO_BRANCH; $false = $(git symbolic-ref --short -q HEAD) }[$env:APPVEYOR_REPO_BRANCH -ne $NULL];
$revision = @{ $true = "{0:00000}" -f [convert]::ToInt32("0" + $env:APPVEYOR_BUILD_NUMBER, 10); $false = "local" }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
$suffix = @{ $true = ""; $false = "$($branch.Substring(0, [math]::Min(10,$branch.Length)))-$revision"}[$branch -eq "master" -and $revision -ne "local"]
$commitHash = $(git rev-parse --short HEAD)
$buildSuffix = @{ $true = "$($suffix)-$($commitHash)"; $false = "$($branch)-$($commitHash)" }[$suffix -ne ""]

Write-Host "build: Package version suffix is $suffix"
Write-Host "build: Build version suffix is $buildSuffix" 

foreach ($src in Get-ChildItem src/*) {
    Push-Location $src

    Write-Host "build: Packaging project in $src"

    & dotnet build -c Release --version-suffix=$buildSuffix
    if ($suffix) {
        & dotnet pack -c Release --include-source -o ..\..\artifacts --version-suffix=$suffix --no-build
    } else {
        & dotnet pack -c Release --include-source -o ..\..\artifacts --no-build
    }
    if($LASTEXITCODE -ne 0) { exit 1 }    

    Pop-Location
}

foreach ($sample in Get-ChildItem sample/*) {
    Push-Location $sample

    Write-Host "build: Testing project in $sample"

    & dotnet build -c Release --version-suffix=$buildSuffix
    if($LASTEXITCODE -ne 0) { exit 3 }

    Pop-Location
}

foreach ($test in ls test/*.Tests) {
    Push-Location $test

	Write-Host "build: Testing project in $test"

    & dotnet test -c Release
    if($LASTEXITCODE -ne 0) { exit 3 }

    Pop-Location
}

Pop-Location
