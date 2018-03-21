#if UNITY_IOS

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using System.Text;

/// <summary>
/// Helper editor class for updating the generated XCode project's Info.plist file.
/// </summary>
public static class XcodePostProcessor
{
	[PostProcessBuild]
	public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
	{
		if (buildTarget != BuildTarget.iOS)
		{
			return;
		}

		ModifyXcodeProject(pathToBuiltProject);
	}

	private static void ModifyXcodeProject (string pathToBuiltProject)
	{
		var projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		var proj = new PBXProject();

		proj.ReadFromFile(projPath);

		string targetGUID = proj.TargetGuidByName("Unity-iPhone");

		proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");

		proj.AddFrameworkToProject (targetGUID, "StoreKit.framework", false);
		proj.AddFrameworkToProject (targetGUID, "WebKit.framework", false);

		File.WriteAllText(projPath, proj.WriteToString());
	}
}

#endif