using ABFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass_ABToolFrameWork : MonoBehaviour
{
    private string sceneName = "Scene1";
    private string assetBundleName = "scene1/prefabs.ab";
    private string assetName = "jieni.prefab";

    // Start is called before the first frame update
    void Start()
    {
        //调用AB包（自动调用依赖项）
        StartCoroutine(AssetBundleMgr.GetInstance().LoadAssetBundlePack(sceneName, assetBundleName, LoadAllAbComplete));
    }

    /// <summary>
    /// 回调函数：所有AB包加载完毕
    /// </summary>
    /// <param name="abName"></param>
    private void LoadAllAbComplete(string abName)
    {
        Object tempObj = null;
        tempObj = AssetBundleMgr.GetInstance().LoadAsset(sceneName, assetBundleName, assetName, false);
        if (tempObj != null)
        {
            Instantiate(tempObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AssetBundleMgr.GetInstance().DisposeAllAsset(sceneName);
        }
    }
}
