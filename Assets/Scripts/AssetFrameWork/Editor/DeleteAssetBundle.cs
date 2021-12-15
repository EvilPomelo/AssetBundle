using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABFW
{
    public class DeleteAssetBundle
    {
        /// <summary>
        /// 批量删除AB包文件
        /// </summary>
        [MenuItem("AssetBundleTools/DeleteAllAssetBundles")]
        public static void DelAssetBundle()
        {

            string strNeedDeleteDIR = PathTools.GetABOutPath();
            if (!string.IsNullOrEmpty(strNeedDeleteDIR))
            {
                //true代表可以删除非空目录
                Directory.Delete(strNeedDeleteDIR, true);
                File.Delete(strNeedDeleteDIR + ".meta");

                AssetDatabase.Refresh();
            }
        }
    }
}