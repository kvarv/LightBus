param(
    [Parameter(Position=0,Mandatory=0)]
    [string[]]$tasks = @(),
    [Parameter(Position=1,Mandatory=0)]
    [string]$version
)

.\tools\psake\psake .\build\build.ps1 -taskList $tasks -parameters @{version=$version} ; if ($psake.build_success -eq $false) { write-host "ERROR" -fore RED; exit 1 } else { exit 0 }