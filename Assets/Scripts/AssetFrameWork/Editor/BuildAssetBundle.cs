﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ABFW
{
    public class BuildAssetBundle
    {
        /// <summary>
        /// 打包所有WIndows AB包
        /// </summary> 
        [MenuItem("AssetBundleTools/BuildAllAB-Windows")]
        public static void BuildAllABWindows()
        {
            //获取AB包输出文件夹
            string strABOutPathDIR = PathTools.GetABOutUnityPath(RuntimePlatform.WindowsPlayer);

            //判断生成输出目录
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }


            //打包
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

        /// <summary>
        /// 打包所有安卓AB包
        /// </summary> 
        [MenuItem("AssetBundleTools/BuildAllAB-Android")]
        public static void BuildAllABAndroid()
        {
            //获取AB包输出文件夹
            string strABOutPathDIR = PathTools.GetABOutUnityPath(RuntimePlatform.Android);

            //判断生成输出目录
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }

            //打包
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        /// <summary>
        /// 打包所有安卓AB包
        /// </summary> 
        [MenuItem("AssetBundleTools/BuildAllAB-IOS")]
        public static void BuildAllABIOS()
        {
            //获取AB包输出文件夹
            string strABOutPathDIR = PathTools.GetABOutUnityPath(RuntimePlatform.IPhonePlayer);

            //判断生成输出目录
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }


            //打包
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.iOS);
        }
    }
}