using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABFW
{
    public class ReNameFloderAsset
    {

        [MenuItem("Assets/Directory/Rename", false, 10)]
        public static void ReNamebyFloderName()
        {
            string[] strs = Selection.assetGUIDs;

            if (strs.Length == 0)
            {
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(strs[0]);

            int pathIdex = path.LastIndexOf("/");
            string name = path.Substring(pathIdex + 1);


            DirectoryInfo dirTempInfo = new DirectoryInfo(path);
            FileSystemInfo[] fileInfo = dirTempInfo.GetFiles();
            int i = 1;
            foreach (var file in fileInfo)
            {
                if (file.Extension != ".meta")
                {
                    int tempIndex = file.FullName.IndexOf("Assets");
                    string filePath = file.FullName.Substring(tempIndex);
                    AssetDatabase.RenameAsset(filePath, name + i.ToString());
                    i++;
                }
            }
        }
    }
}