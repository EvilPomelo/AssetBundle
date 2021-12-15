﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class PathTools
    {
        /// <summary>
        /// 资源路径常量
        /// </summary>
        private const string AB_RESOURCES = "AB_Resources";

        /// <summary>
        /// 获取AB资源目录
        /// </summary>
        /// <returns></returns>
        public static string GetABResourcePath()
        {
            return Application.dataPath + "/" + AB_RESOURCES;
        }


        /// <summary>
        /// 获取下载AB包路径
        /// </summary>
        /// <returns></returns>
        public static string GetAbDownloadPath()
        {
            string strABDownLoadPath = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strABDownLoadPath = GetABOutPath();
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strABDownLoadPath = GetABOutPath();
                    break;
                default:
                    break;
            }
            Debug.Log("strABDownLoadPath" + strABDownLoadPath);
            return strABDownLoadPath;
        }

        /// <summary>
        /// 获取AB包输出路径
        /// </summary>
        /// <returns></returns>
        public static string GetABOutPath()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }

        /// <summary>
        /// 获取Application.streamingAssetsPath
        /// </summary>
        /// <returns></returns>
        public static string GetABOutUnityPath()
        {
            return Application.streamingAssetsPath + "/" + GetPlatformName();
        }

        /// <summary>
        /// 获取本地打包路径
        /// </summary>
        /// <returns></returns>
        public static string GetABOutUnityPath(RuntimePlatform platform)
        {
            return Application.streamingAssetsPath + "/" + GetPlatformName(platform);
        }

        /// <summary>
        /// 获取平台主要存储路径
        /// streamingAssetsPath是可以跟随Unity打包存在的文件夹，WWW和IO都可以访问这个文件夹下的文件，但是这个文件夹是只读文件夹，不支持修改文件和写入等操作。
        /// persistentDataPath是可读写的文件夹，但是这个文件夹是在APK安装成功后创建的，所以无法将文件在打包前放入此文件夹。
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformPath()
        {
            string strReturnPlatformPath = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformPath = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnPlatformPath = Application.persistentDataPath;
                    break;
                default:
                    break;
            }
            return strReturnPlatformPath;
        }


        /// <summary>
        /// 获取当前运行平台的名称
        /// </summary>
        /// <returns></returns>
        private static string GetPlatformName()
        {
            string strReturnPlatformName = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformName = "Windows";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnPlatformName = "Iphone";
                    break;
                case RuntimePlatform.Android:
                    strReturnPlatformName = "Android";
                    break;
                default:
                    break;
            }
            return strReturnPlatformName;
        }

        private static string GetPlatformName(RuntimePlatform platform)
        {
            string strReturnPlatformName = string.Empty;
            switch (platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformName = "Windows";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnPlatformName = "Iphone";
                    break;
                case RuntimePlatform.Android:
                    strReturnPlatformName = "Android";
                    break;
                default:
                    break;
            }
            return strReturnPlatformName;
        }
    }
}
