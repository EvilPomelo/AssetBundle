using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class MultiABMgr
    {
        /// <summary>
        /// 引用类：单个AB包加载实现类
        /// </summary>
        private SingleABLoader currentSingleABLoader;
        /// <summary>
        /// AB包实现类缓存集合(缓存AB包，防止重复加载)
        /// </summary>
        private Dictionary<string, SingleABLoader> dicSingleABLoaderCache;
        /// <summary>
        /// 当前场景
        /// </summary>
        private string currentSceneName;
        /// <summary>
        /// 当前AssetBundle名称
        /// </summary>
        private string currentABName;
        /// <summary>
        /// AB包对应依赖关系集合
        /// </summary>
        private Dictionary<string, ABRelation> dicABRelation;
        /// <summary>
        /// 所有AB包加载完成
        /// </summary>
        private Action<string> loadAllABPackageCompleteHandle;


        /// <summary>
        /// 构造函数
        /// </summary>
        public MultiABMgr(string sceneName, string abName, Action<string> loadAllABPackageCompleteHandle)
        {
            currentSceneName = sceneName;
            currentABName = abName;
            dicSingleABLoaderCache = new Dictionary<string, SingleABLoader>();
            dicABRelation = new Dictionary<string, ABRelation>();
            this.loadAllABPackageCompleteHandle = loadAllABPackageCompleteHandle;
        }

        /// <summary>
        /// 完成指定AB包调用
        /// </summary>
        /// <param name="abName"></param>
        private void CompleteLoadAB(string abName)
        {
            if (abName.Equals(currentABName))
            {
                if (loadAllABPackageCompleteHandle != null)
                {
                    loadAllABPackageCompleteHandle(abName);
                }
            }
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle(string abName)
        {
            //AB包关系的建立
            if (!dicABRelation.ContainsKey(abName))
            {
                ABRelation abRelationObj = new ABRelation(abName);
                dicABRelation.Add(abName, abRelationObj);
            }
            ABRelation tempABRelationObj = dicABRelation[abName];

            string[] strDependeceArray = ABManifestLoader.GetInstace().RetrivalDependce(abName);
            //得到指定AB包所有的依赖关系
            foreach (string itemDenpence in strDependeceArray)
            {
                //添加依赖项
                tempABRelationObj.AddDenpendece(itemDenpence);
                //添加引用项
                yield return LoadReference(itemDenpence, abName);

            }
            //真正加载AB包
            if (dicSingleABLoaderCache.ContainsKey(abName))
            {
                yield return dicSingleABLoaderCache[abName].LoadAssetBundleLocal(CompleteLoadAB);
            }
            else
            {
                currentSingleABLoader = new SingleABLoader(abName);
                dicSingleABLoaderCache.Add(abName, currentSingleABLoader);
                yield return currentSingleABLoader.LoadAssetBundleLocal(CompleteLoadAB);
            }


            yield return null;
        }

        /// <summary>
        /// 加载引用AB包
        /// </summary>
        /// <param name="abName">AB包名称</param>
        /// <param name="refABName">被引用AB包名称</param>
        /// <returns></returns>
        private IEnumerator LoadReference(string abName, string refABName)
        {
            //AB包已经加载
            if (dicABRelation.ContainsKey(abName))
            {
                ABRelation tempABRelationObj = dicABRelation[abName];
                //添加AB包引用关系（被依赖）
                tempABRelationObj.AddReferences(refABName);
            }
            else
            {
                ABRelation tempABRelationObj = new ABRelation(abName);
                tempABRelationObj.AddReferences(refABName);
                dicABRelation.Add(abName, tempABRelationObj);
                yield return LoadAssetBundle(abName);
            }
        }

        /// <summary>
        /// 加载AB包中资源
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string abName, string assetName, bool isCache)
        {
            foreach (var itermABName in dicSingleABLoaderCache.Keys)
            {
                if (abName == itermABName)
                {
                    return dicSingleABLoaderCache[itermABName].LoadAsset(assetName, isCache);
                }
            }

            Debug.LogError(GetType() + "/LoadAsset()/找不到AssetBundle包，请检查！abName=" + abName + ",assetname=" + assetName);

            return null;
        }


        /// <summary>
        /// 释放本场景中所有资源
        /// </summary>
        public void DisposeAllAsset()
        {
            try
            {
                //逐一释放所有加载过的assetbundle包中的资源
                foreach (SingleABLoader itemABLoader in dicSingleABLoaderCache.Values)
                {
                    itemABLoader.Disposeall();
                }
            }
            finally
            {
                dicSingleABLoaderCache.Clear();
                dicSingleABLoaderCache = null;

                //释放其他对象占用资源
                dicABRelation.Clear();
                dicABRelation = null;

                currentSceneName = null;
                currentABName = null;
                loadAllABPackageCompleteHandle = null;

                //卸载没有使用到的资源
                Resources.UnloadUnusedAssets();
                //强制垃圾收集
                System.GC.Collect();
            }
        }
    }
}