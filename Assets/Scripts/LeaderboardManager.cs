using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LeaderboardManager : MonoBehaviour
{
    string url = "http://flappybaichikapi.akmatoff.repl.co/api/records/";
    string token = "8d0b3ca88b4f22b0eb717590bd3dc4312d7a4feb";
    void Start()
    {
        StartCoroutine(FetchRecords());
    }

    IEnumerator FetchRecords() {
        UnityWebRequest getRecords = UnityWebRequest.Get(url);
        getRecords.SetRequestHeader("Content-Type", "application/json");
        getRecords.SetRequestHeader("Accept", "application/json");
        getRecords.SetRequestHeader("Authorization", "Token " + token);
        yield return getRecords.SendWebRequest();

        if (getRecords.responseCode == 200) {
            Debug.Log("Data fetched!");
            RecordModel record = JsonConvert.DeserializeObject<RecordModel>(getRecords.downloadHandler.text);
            print(record);
        } else {
            Debug.Log(getRecords.error);
        }

    }

}
