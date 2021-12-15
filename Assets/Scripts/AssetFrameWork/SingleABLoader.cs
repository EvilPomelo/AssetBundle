using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ABFW
{
    public class SingleABLoader : IDisposable
    {
        /// <summary>
        ///引用类，资源加载类
        /// </summary>
        private AssetLoader assetLoader;

        /// <summary>
        /// AB包名称
        /// </summary>
        private string abName;

        /// <summary>
        /// 下载路径
        /// </summary>
        private string abServerPath;

        private string abLocalPath;

        /// <summary>
        /// 初始化ab包名字和下载路径
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="path"></param>
        public SingleABLoader(string assetBundleName, string path = null)
        {
            abName = assetBundleName;
            if (path == null)
            {
                abLocalPath = PathTools.GetAbLocalPath() + "/" + abName;
                abServerPath = PathTools.GetAbServerPath() + "/" + abName;
            }
            else
            {
                abServerPath = path;
                abLocalPath = path;
            }
        }

        /// <summary>
        /// 本地读取ASSETBUNDLE
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadAssetBundleLocal(Action<string> LoadComplete)
        {
            if (string.IsNullOrEmpty(abName))
            {
                Debug.LogError(GetType() + "abName输入参数不合法！");
            }

            AssetBundleCreateRequest abRequest = AssetBundle.LoadFromFileAsync(abLocalPath);
            yield return abRequest;
            AssetBundle ab = abRequest.assetBundle;

            if (ab != null)
            {
                assetLoader = new AssetLoader(ab);
                if (LoadComplete != null)
                {
                    LoadComplete?.Invoke(abName);
                }
            }


        }

        /// <summary>
        /// 通过UnityWebRequest读取ASSETBUNDLE
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle(Action<string> LoadComplete)
        {
            if (string.IsNullOrEmpty(abName))
            {
                Debug.LogError(GetType() + "abName输入参数不合法！");
            }
            Directory.CreateDirectory(@"E:\UnityProject\AssetBundle\Cache");

            Caching.currentCacheForWriting = Caching.AddCache(@"E:\UnityProject\AssetBundle\Cache");

            using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(abServerPath, 2, 0))
            {
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(GetType() + "/ERROR/" + request.error);
                }
                else
                {
                    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                    if (ab != null)
                    {
                        assetLoader = new AssetLoader(ab);
                        if (LoadComplete != null)
                        {
                            LoadComplete?.Invoke(abName);
                        }
                    }
                    else
                    {
                        Debug.LogError(GetType() + "/ERROR/" + "未获取到AB包");
                    }
                }
            }
        }


        /// <summary>
        /// 加载AB资源包
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName, bool isCache)
        {
            if (assetLoader != null)
            {
                return assetLoader.LoadAsset(assetName, isCache);
            }
            Debug.LogError(GetType() + "/LoadAsset()/" + "assetLoader==null!");
            return null;
        }

        /// <summary>
        /// 卸载AB资源包
        /// </summary>
        /// <param name="asset"></param>
        public void UnloadAsset(UnityEngine.Object asset)
        {
            if (assetLoader != null)
            {
                assetLoader.UnLoadAsset(asset);
            }
            else
            {
                Debug.LogError(GetType() + "/UnloadAsset()/参数 assetLoader==null,请检查！");
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (assetLoader != null)
            {
                assetLoader.Dispose();
                assetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnloadAsset()/参数 assetLoader==null,请检查！");
            }
        }

        //释放当前AssetBundle资源包且卸载所有资源引用
        public void Disposeall()
        {
            if (assetLoader != null)
            {
                assetLoader.DisposeAll();
                assetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnloadAsset()/参数 assetLoader==null,请检查！");
            }
        }

        /// <summary>
        /// 查询当前AssetBundle包中所有资源
        /// </summary>
        /// <returns></returns>
        public string[] RetrivalAllAssetName()
        {
            if (assetLoader != null)
            {
                return assetLoader.RetriveAllAssetName();
            }
            Debug.LogError(GetType() + "/Dispose()/参数 assetLoader==null,q请检查！");
            return null;
        }

    }
}