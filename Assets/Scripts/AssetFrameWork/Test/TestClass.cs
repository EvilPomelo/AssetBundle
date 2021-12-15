using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class TestClass : MonoBehaviour
    {
        SingleABLoader loadObj = null;
        string abName = "scene1/prefabs.ab";
        string assetName = "jieni.prefab";
        string abName2 = "scene1/materials.ab";
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Application.persistentDataPath);

            loadObj = new SingleABLoader(abName2);
            StartCoroutine(loadObj.LoadAssetBundle(LoadComplete2));

            loadObj = new SingleABLoader(abName);
            StartCoroutine(loadObj.LoadAssetBundle(LoadComplete));


        }

        public void LoadComplete(string abName)
        {
            Debug.Log("回调函数");
            Object tempObj = loadObj.LoadAsset(assetName, false);
            Instantiate(tempObj);
        }

        public void LoadComplete2(string abName)
        {
            Debug.Log("回调函数");
            //Object tempObj = loadObj.LoadAsset(assetName, false);
            //Instantiate(tempObj);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}