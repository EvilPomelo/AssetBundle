using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ABFW
{
    public class ABManifestLoader : IDisposable
    {
        /// <summary>
        /// 本类实例
        /// </summary>
        private static ABManifestLoader instance;

        /// <summary>
        /// AssetBundle(清单文件)系统类
        /// </summary>
        private AssetBundleManifest manifestObj;

        /// <summary>
        /// 读取AB清单文件的路径
        /// </summary>
        private string strManifestPath;

        /// <summary>
        /// 获取ab包清单的assetBundle
        /// </summary>
        private AssetBundle abReadManifest;

        /// <summary>
        /// 是否加载完成
        /// </summary>
        private bool isLoadFinsh;

        /// <summary>
        /// 只读属性
        /// </summary>
        public bool IsLoadFinsh
        {
            get { return isLoadFinsh; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ABManifestLoader()
        {
            //strManifestPath = PathTools.GetAbLocalPath();
            //strManifestPath = PathTools.GetABOutUnityPath(RuntimePlatform.Android) + "//Android";
            strManifestPath = PathTools.GetAbLocalPath() + "//Android";
            manifestObj = null;
            abReadManifest = null;
            isLoadFinsh = false;
        }


        /// <summary>
        /// 获取本类实例
        /// </summary>
        /// <returns></returns>
        public static ABManifestLoader GetInstace()
        {
            if (instance == null)
            {
                instance = new ABManifestLoader();
            }
            return instance;
        }

        /// <summary>
        /// 读取清单文件
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadMainfestFile()
        {
            AssetBundleCreateRequest abRequest = AssetBundle.LoadFromFileAsync(strManifestPath);
            yield return abRequest;
            AssetBundle abObj = abRequest.assetBundle;
            if (abObj != null)
            {
                abReadManifest = abObj;
                //读取清单文件
                manifestObj = abObj.LoadAsset<AssetBundleManifest>(ABDefine.AssetBundle_MANIFEST);
                //加载完成
                isLoadFinsh = true;
            }
            else
            {
                Debug.Log(GetType() + "/LoadMainfestFile()/AssetBundle读取失败！strManifestPath：" + strManifestPath);
            }
        }

        /// <summary>
        /// 获取AssetBundleManifest系统类实例
        /// </summary>
        /// <returns></returns>
        public AssetBundleManifest GetABManifest()
        {
            if (isLoadFinsh)
            {
                if (manifestObj != null)
                {
                    return manifestObj;
                }
                else
                {
                    Debug.Log(GetType() + "/GetABManifest()/manifestObj==null!");
                }
            }
            else
            {
                Debug.Log(GetType() + "/GetABManifest()/isLoadFinsh==false!,Manifest未加载完毕，请检查");
            }
            return null;
        }

        /// <summary>
        /// 获取AssetBundleManifest(系统类)所有依赖项
        /// </summary>
        /// <param name="abName">AB包名称</param>
        /// <returns></returns>
        public string[] RetrivalDependce(string abName)
        {
            if (manifestObj != null && !string.IsNullOrEmpty(abName))
            {
                return manifestObj.GetAllDependencies(abName);
            }
            return null;
        }


        /// <summary>
        /// 释放本类资源
        /// </summary>
        public void Dispose()
        {
            if (abReadManifest != null)
            {
                abReadManifest.Unload(true);
            }
        }
    }
}