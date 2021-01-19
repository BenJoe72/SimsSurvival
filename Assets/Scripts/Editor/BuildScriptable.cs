using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildScript", menuName = "BuildScript", order = 9999)]
public class BuildScriptable : ScriptableObject
{
    public string ExecutableName;
    public BuildTarget Target;
    public BuildOptions Options;
    public SceneAsset[] AddedScenes;

    [Header("Butler")]
    public bool PublishButler;
    public string ButlerAccount;
    public string ButlerGame;

    private const string BUILDPATHFORMAT = "Builds/{2}/Build {0}/{1}";

    public void BuildProject(string version)
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = AddedScenes.Select(x => AssetDatabase.GetAssetPath(x)).ToArray();
        options.locationPathName = GetPath(version);
        options.target = Target;
        options.options = Options;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");

            if (PublishButler)
            {
                RunButler(version);
            }
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    public void RunButler(string version)
    {
        // Compress folder
        string compressCommand = "Compress-Archive -Path \\\"{0}\\\" -DestinationPath \\\"{1}\\\" -Force";
        string path = GetFullPath(version).Replace("/", "\\");
        string zippath = path + "_" + GetChannel() + ".zip";
        int result = CommandLineHelper.RunPowershellLine(string.Format(compressCommand, path+"\\*", zippath));

        if (result == 0)
            Debug.Log("Zip file created successfully.");
        else
            Debug.LogError("ERROR while creating zip file.");

        // Upload with butler
        string butlerCommand = "butler push \\\"{0}\\\" {1}/{2}:{3}";
        result = CommandLineHelper.RunPowershellLine(string.Format(butlerCommand, zippath, ButlerAccount, ButlerGame, GetChannel()));

        if (result == 0)
            Debug.Log("Game successfully uploaded to itch.io");
        else
            Debug.LogError("ERROR while uploading game.");
    }

    private string GetFullPath(string version)
    {
        string path = string.Format(BUILDPATHFORMAT, version, ExecutableName, Target.ToString());
        return System.IO.Path.Combine(Application.dataPath.Replace("/Assets", string.Empty), path);
    }

    private string GetPath(string version)
    {
        string result = string.Format(BUILDPATHFORMAT, version, ExecutableName, Target.ToString());

        switch (Target)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                result += "/"+ExecutableName+".exe";
                break;
            default:
                break;
        }

        return result;
    }

    private string GetChannel()
    {
        switch (Target)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "win";
            case BuildTarget.WebGL:
                return "html";
            default:
                return "none";
        }
    }
}

public class BuildWindow : EditorWindow
{
    [SerializeField] public BuildScriptable Script;
    [SerializeField] public string Version;

    private static GUIStyle HeaderStyle;

    private void OnEnable()
    {
        Initialize();
    }

    [MenuItem("Tools/Build Window")]
    public static void OpenWindow()
    {
        var window = GetWindow<BuildWindow>("Build Window");

        Initialize();
    }

    private static void Initialize()
    {
        HeaderStyle = new GUIStyle();
        HeaderStyle.normal.textColor = Color.white;
        HeaderStyle.alignment = TextAnchor.MiddleCenter;
        HeaderStyle.fontSize = 20;
    }

    private void OnGUI()
    {
        float width = EditorGUIUtility.currentViewWidth - (EditorGUIUtility.currentViewWidth / 3f);

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Build Settings", HeaderStyle);

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();

        Script = EditorGUILayout.ObjectField("Build configuration", Script, typeof(BuildScriptable), false, GUILayout.Width(width), GUILayout.Height(25)) as BuildScriptable;

        if (Script != null)
        {
            Version = EditorGUILayout.TextField("Version number", Version, GUILayout.Width(width), GUILayout.Height(25));

            GUILayout.Space(20);
            if (GUILayout.Button("Build Project", GUILayout.Width(width), GUILayout.Height(50)))
            {
                Script.BuildProject(Version);
            }
            if (GUILayout.Button("Upload Project", GUILayout.Width(width/2), GUILayout.Height(25)))
            {
                Script.RunButler(Version);
            }
        }

        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}