$ErrorActionPreference = "Stop"

# 1. Unity 실행 경로 및 프로젝트 경로
$unityPath = "C:\Program Files\Unity\Hub\Editor\2022.3.62f3\Editor\Unity.exe"
$projectPath = $PSScriptRoot
$logPath = Join-Path $projectPath "unity-build.log"

# 2. Unity Editor 존재 여부 확인
if (-not (Test-Path $unityPath)) {
    throw "Unity Editor를 찾을 수 없습니다: $unityPath"
}

# 3. Unity CLI 실행 인자
$arguments = @(
    "-batchmode"
    "-nographics"
    "-quit"
    "-projectPath `"$projectPath`""
    "-buildTarget WebGL"
    "-executeMethod BuildScript.BuildWebGL"
    "-logFile `"$logPath`""
)

Write-Host "Unity WebGL 빌드를 시작합니다."

# 4. Unity 프로세스 실행
$process = Start-Process `
    -FilePath $unityPath `
    -ArgumentList $arguments `
    -PassThru

if ($null -eq $process) {
    throw "Unity 프로세스를 시작하지 못했습니다."
}

# 5. 경과 시간 + 회전 애니메이션
$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
$frames = @("|", "/", "-", "\")
$frameIndex = 0

while (-not $process.HasExited) {
    $elapsed = $stopwatch.Elapsed.ToString("hh\:mm\:ss")
    $frame = $frames[$frameIndex % $frames.Length]

    Write-Host "`r[$frame] Unity WebGL 빌드 중... 경과 시간: $elapsed   " -NoNewline

    Start-Sleep -Milliseconds 250
    $frameIndex++

    $process.Refresh()
}

$stopwatch.Stop()

Write-Host ""
Write-Host "Unity 프로세스 종료 (소요 시간: $($stopwatch.Elapsed.ToString('hh\:mm\:ss')))"

# 6. 종료 코드 확인
$exitCode = $process.ExitCode

Write-Host "Unity Exit Code: $exitCode"

if ($exitCode -ne 0) {
    Write-Host "===== Unity Build Log ====="

    if (Test-Path $logPath) {
        Get-Content $logPath -Tail 50
    }

    throw "Unity WebGL 빌드 실패 (Exit Code: $exitCode)"
}

# 7. 빌드 결과물 확인
$buildIndex = Join-Path $projectPath "Build\WebGL\index.html"

if (-not (Test-Path $buildIndex)) {
    throw "빌드 결과물을 찾을 수 없습니다: $buildIndex"
}

Write-Host "Unity WebGL 빌드 완료!"
Write-Host "빌드 결과물: $buildIndex"