using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Linq;

public static class BuildScript
{
	public static void BuildWebGL()
	{
		// Unity 프로젝트 루트 경로
		string projectPath = Path.GetFullPath(
			Path.Combine(Application.dataPath, "..")
		);

		// WebGL 빌드 결과물 저장 경로
		string buildPath = Path.Combine(
			projectPath,
			"Build",
			"WebGL"
		);

		// Build Settings에 등록된 활성 씬
		string[] scenes = Array.ConvertAll(
			EditorBuildSettings.scenes
				.Where(scene => scene.enabled)
				.ToArray(),
			scene => scene.path
		);

		BuildPlayerOptions options = new BuildPlayerOptions
		{
			scenes = scenes,
			locationPathName = buildPath,
			target = BuildTarget.WebGL,
			options = BuildOptions.None
		};

		BuildReport report =
			BuildPipeline.BuildPlayer(options);

		if (report.summary.result != BuildResult.Succeeded)
		{
			throw new Exception("WebGL Build Failed");
		}

		Debug.Log("WebGL Build Succeeded");
	}
}
