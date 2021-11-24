using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
    

public class BuildAssetBundle
{
    /// <summary>
    /// 打包所有AB包
    /// </summary>
    [MenuItem("CustomTools/AssetBundle/BuildAllAB")]
    public static void BuildAllAB()
    {
        string strABOutPathDIR = string.Empty;
        strABOutPathDIR = Application.streamingAssetsPath;

        if (!Directory.Exists(strABOutPathDIR))
        {
            Directory.CreateDirectory(strABOutPathDIR);
        }

        BuildPipeline.BuildAssetBundles(strABOutPathDIR,BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows);
    }
}
