using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoadDemo : MonoBehaviour
{

    IEnumerator LoadNonObjectFromAB(string ABURL,GameObject goShowObj,string assetName)
    {
        if (string.IsNullOrEmpty(ABURL) || goShowObj == null)
        {
            Debug.LogError(GetType() + "LoadNonObjectFromAB输入参数不合法！");
        }

        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(ABURL))
        {
            yield return request.SendWebRequest();
            if (request.result!= UnityWebRequest.Result.Success)
            {
                Debug.LogError(GetType() + "/ERROR/" + request.error);
            }
            else
            {
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                ab.mainAsset
            }
        }
    }
}
