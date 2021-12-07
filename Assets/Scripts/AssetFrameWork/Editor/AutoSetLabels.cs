using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABFW
{
    public class AutoSetLabels
    {

        /// <summary>
        /// 给AB包自动打标记
        /// </summary>
        [MenuItem("AssetBundleTools/Set AB Label")]
        public static void SetABLabel()
        {
            //删除无用标记
            AssetDatabase.RemoveUnusedAssetBundleNames();

            //获取主资源文件夹信息
            DirectoryInfo dirTempInfo = new DirectoryInfo(PathTools.GetABResourcePath());

            //获取主资源文件夹下的场景资源文件夹
            DirectoryInfo[] dirSceneDirArry = dirTempInfo.GetDirectories();

            //遍历场景资源文件夹
            foreach (var mainDir in dirSceneDirArry)
            {
                //获取场景资源文件夹下资源分类
                DirectoryInfo[] dirArryTemp = mainDir.GetDirectories();
                //递归遍历资源类型文件夹下所有文件，并按 “场景文件夹名/资源类型文件夹” 为标记名称给其打标记
                foreach (var dir in dirArryTemp)
                {
                    SetABLabelInDirectory(dir, mainDir.Name + "/" + dir.Name);
                }
            }

            //刷新资源状态
            AssetDatabase.Refresh();

            Debug.Log("标记完成");
        }

        /// <summary>
        /// 设置对应资源文件夹的打包
        /// </summary>
        /// <param name="dir">当前文件夹</param>
        /// <param name="labeName">标记名称</param>
        private static void SetABLabelInDirectory(DirectoryInfo dir, string labeName)
        {
            //获取当前文件夹下所有文件
            FileSystemInfo[] fileInfo = dir.GetFiles();
            //遍历当前文件夹下所有文件
            foreach (var file in fileInfo)
            {
                //不对扩展名为".meta"的文件做标记
                if (file.Extension != ".meta")
                {
                    //获取unity Aseets文件的相对路径
                    int tempIndex = file.FullName.IndexOf("Assets");
                    string tempFilePath = file.FullName.Substring(tempIndex);
                    //通过AssetImporter给对应资源打标记
                    AssetImporter tempImportObj = AssetImporter.GetAtPath(tempFilePath);
                    tempImportObj.assetBundleName = labeName;
                    //后缀名为.unity，变体为"u3d"，否则为ab
                    if (file.Extension == ".unity")
                    {
                        tempImportObj.assetBundleVariant = "u3d";
                    }
                    else
                    {
                        tempImportObj.assetBundleVariant = "ab";
                    }
                }
            }

            //获取当前文件夹内的子文件夹数量
            DirectoryInfo[] dirSceneDirArry = dir.GetDirectories();

            //如果拥有子文件夹，则递归子文件夹
            if (dirSceneDirArry.Length != 0)
            {
                foreach (var directory in dirSceneDirArry)
                {
                    SetABLabelInDirectory(directory, labeName);
                }
            }
        }



    }
}