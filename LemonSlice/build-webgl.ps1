$ErrorActionPreference = "Stop"

# Unity Editor 실행 파일 경로
$unityPath = "C:\Program Files\Unity\Hub\Editor\2022.3.62f3\Editor\Unity.exe"

# 현재 스크립트가 위치한 디렉터리
$projectPath = $PSScriptRoot

# 빌드 로그 저장 경로
$logPath = Join-Path $projectPath "unity-build.log"

# Unity Editor 실행 파일 확인
if (-not (Test-Path $unityPath)) {
    throw "Unity Editor를 찾을 수 없습니다: $unityPath"
}

# Unity CLI 실행
& $unityPath `
    -batchmode `
    -quit `
    -projectPath $projectPath `
    -buildTarget WebGL `
    -executeMethod BuildScript.BuildWebGL `
    -logFile $logPath

# 종료 코드 확인
$exitCode = $LASTEXITCODE

if ($exitCode -ne 0) {
    throw "Unity WebGL 빌드 실패 (Exit Code: $exitCode)"
}

Write-Host "Unity WebGL 빌드 완료"