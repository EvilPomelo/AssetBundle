using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ABFW
{
    public class BuildAssetBundle
    {
        /// <summary>
        /// 打包所有AB包
        /// </summary> 
        [MenuItem("AssetBundleTools/BuildAllAB")]
        public static void BuildAllAB()
        {
            //获取AB包输出文件夹
            string strABOutPathDIR = PathTools.GetABOutPath();

            //判断生成输出目录
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }

            //打包
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
}