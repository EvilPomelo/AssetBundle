/***
 * 
 *  Title:
 * 
 *  Description:
 *  
 *  Author:
 *  
 *  Date:
 *  
 *  Modify:
 ***/


using ABFW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class AssetBundleMgr : MonoBehaviour
    {
        //本类实例
        private static AssetBundleMgr instance;
        //场景集合
        private Dictionary<string, MultiABMgr> dicAllScenes = new Dictionary<string, MultiABMgr>();
        //AssetBundle（清单文件）系统类
        private AssetBundleManifest manifestObj = null;

        public AssetBundleMgr()
        {

        }


        public static AssetBundleMgr GetInstance()
        {
            if (instance == null)
            {
                instance = new GameObject("AssetBundleMgr").AddComponent<AssetBundleMgr>();
            }
            return instance;
        }

        private void Awake()
        {
            //加载Manifest清单文件
            StartCoroutine(ABManifestLoader.GetInstace().LoadMainfestFile());
        }

        public IEnumerator LoadAssetBundlePack(string scenesName, string abName, Action<string> loadAllCompleteHandle)
        {
            //参数检查
            if (string.IsNullOrEmpty(scenesName) || string.IsNullOrEmpty(abName))
            {
                Debug.LogError(GetType() + "/LoadAssetBundlePack()/SceneName Or abName is null,请检查");
                yield return null;
            }

            //等待Manifest清单文件加载完成
            while (!ABManifestLoader.GetInstace().IsLoadFinsh)
            {
                yield return null;
            }

            manifestObj = ABManifestLoader.GetInstace().GetABManifest();

            if (manifestObj == null)
            {
                Debug.LogError(GetType() + "/LoadAssetBundlePack()/manifestObj is null,请先确保加载Manifest清单文件!");
            }

            //把当前场景加入集合中
            if (!dicAllScenes.ContainsKey(scenesName))
            {
                MultiABMgr multiMgrObj = new MultiABMgr(scenesName, abName, loadAllCompleteHandle);
                dicAllScenes.Add(scenesName, multiMgrObj);
            }

            //调用下一层（多包管理类）
            MultiABMgr tmpMultiMgrObj = dicAllScenes[scenesName];
            if (tmpMultiMgrObj == null)
            {
                Debug.LogError(GetType() + "/LoadAssetBundlePack()/tmpMultiMgrObj is null,请检查!");
            }

            //调用多包管理类的加载指定AB包
            yield return tmpMultiMgrObj.LoadAssetBundle(abName);
        }

        /// <summary>
        /// 加载AB包中资源
        /// </summary>
        /// <param name="SceneName"></param>
        /// <param name="abName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string SceneName, string abName, string assetName, bool isCache)
        {
            if (dicAllScenes.ContainsKey(SceneName))
            {
                MultiABMgr multiObj = dicAllScenes[SceneName];
                return multiObj.LoadAsset(abName, assetName, isCache);
            }
            Debug.LogError(GetType() + "/LoadAsset()/找不到场景名称,无法加载AB包中资源,请检查!SceneName=" + SceneName);
            return null;
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="SceneName"></param>
        /// <param name="abName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public void DisposeAllAsset(string SceneName)
        {
            if (dicAllScenes.ContainsKey(SceneName))
            {
                MultiABMgr multObj = dicAllScenes[SceneName];
                multObj.DisposeAllAsset();
            }
            else
            {
                Debug.LogError(GetType() + "/DisposeAllAsset()/找不到场景名称,无法释放资源,请检查!SceneName=" + SceneName);
            }
        }
    }
}
