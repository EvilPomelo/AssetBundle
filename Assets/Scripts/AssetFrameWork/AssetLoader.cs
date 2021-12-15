using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class AssetLoader : IDisposable
    {
        /// <summary>
        /// 当前AssetBundle
        /// </summary>
        private AssetBundle currentAssetBundle;

        /// <summary>
        /// 缓存容器集合
        /// </summary>
        private Dictionary<string, AssetBundle> ABDic;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abObj">给定加载的AssetBundle名称</param>
        public AssetLoader(AssetBundle abObj)
        {
            if (abObj != null)
            {
                currentAssetBundle = abObj;
                ABDic = new Dictionary<string, AssetBundle>();
            }
            else
            {
                Debug.Log(GetType() + "/构造函数 AssetBundle()/参数abObj=null!请检查");
            }
        }

        /// <summary>
        /// 加载当前包中指定资源通用方法
        /// </summary>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否开启缓存</param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName, bool isCache = false)
        {

            return LoadResource<UnityEngine.Object>(assetName, isCache);
        }

        /// <summary>
        /// 加载当前包中的AB资源，泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否开启缓存</param>
        /// <returns></returns>
        private T LoadResource<T>(string assetName, bool isCache) where T : UnityEngine.Object
        {
            //缓存中查找
            if (ABDic.ContainsKey(assetName))
            {
                return ABDic[assetName] as T; ;
            }

            //正式加载
            T tmpTResource = currentAssetBundle.LoadAsset<T>(assetName);

            if (tmpTResource != null && isCache)
            {
                ABDic.Add(assetName, tmpTResource as AssetBundle);
            }
            else if (tmpTResource == null)
            {
                Debug.LogError(GetType() + "加载资源- " + assetName + " -失败");
            }

            return tmpTResource;
        }

        /// <summary>
        /// 卸载指定资源
        /// </summary>
        public bool UnLoadAsset(UnityEngine.Object asset)
        {
            if (asset != null)
            {
                Resources.UnloadAsset(asset);
                return true;
            }
            Debug.LogError(GetType() + "asset为空请检查");

            return false;
        }

        /// <summary>
        /// Unload(false)的时候卸载的只是这个包的镜像文件,已经加载的资源不会被删除
        /// </summary>
        public void Dispose()
        {
            currentAssetBundle.Unload(false);
        }


        /// <summary>
        /// Unload(true)的时候卸载的是整个ab包的资源,假如有一个图片是从ab中加载的,那么调用这个方法 这个图片直接会miss
        /// </summary>,
        public void DisposeAll()
        {
            currentAssetBundle.Unload(true);
        }

        /// <summary>
        /// 查询AssetBundle中包含的所有资源
        /// </summary>
        /// <returns></returns>
        public string[] RetriveAllAssetName()
        {
            return currentAssetBundle.GetAllAssetNames();
        }
    }
}