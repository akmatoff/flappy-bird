using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using dotenv.net.Utilities;
using dotenv.net;

public class LeaderboardManager : MonoBehaviour
{
    string url = "http://flappybaichikapi.akmatoff.repl.co/api/records.json";
    string token;
    string jsonStringArray;
    Record[] sortedRecords;
    int recordPosition;
    public GameObject recordElement;
    public Transform leaderboardListContent;
    void Start()
    {
        recordPosition = 1;
        DotEnv.Config(true, ".env"); // Set custom path of the file
        var envReader = new EnvReader(); 
        token = envReader.GetStringValue("TOKEN"); // Get string from dotenv file
        StartCoroutine(FetchRecords()); 
    }

    IEnumerator FetchRecords() {
        UnityWebRequest getRecords = UnityWebRequest.Get(url); // Get request to the API
        getRecords.SetRequestHeader("Content-Type", "application/json"); // Setting the headers
        getRecords.SetRequestHeader("Accept", "application/json");
        getRecords.SetRequestHeader("Authorization", $"Token {token}");
        yield return getRecords.SendWebRequest();

        if (getRecords.responseCode == 200) {
            Debug.Log("Data fetched!");
            jsonStringArray = "{\"records\":" + getRecords.downloadHandler.text + "}"; // Wrap JSON Array
            Records records = JsonUtility.FromJson<Records>(jsonStringArray); // Parse JSON Array
            sort(records.records);
            // Loop through each record
            foreach(var record in records.records) {
                Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y); // Assign record object's position
                Quaternion q = this.transform.rotation; // Assign record object's rotation
                recordElement.GetComponent<RecordElement>().positionText.GetComponent<TextMeshProUGUI>().text = recordPosition.ToString();
                recordElement.GetComponent<RecordElement>().playerNameText.GetComponent<TextMeshProUGUI>().text = record.player; 
                recordElement.GetComponent<RecordElement>().highscoreText.GetComponent<TextMeshProUGUI>().text = record.highscore.ToString();
                GameObject newRecordObject = Instantiate(recordElement, position, q);
                newRecordObject.transform.SetParent(leaderboardListContent);
                recordPosition++;
            }
        } else {
            Debug.Log(getRecords.error);
        }

    }

    static void sort(Record[] array) {
        int len = array.Length; // Get the length of the array

        // Move the boundary of the unsorted array one by one
        for (int i = 0; i < len - 1; i++) {
            // Find the maximum element in unsorted array
            int max_i = i;
            for (int j = i + 1; j < len; j++) {
                if (array[j].highscore > array[max_i].highscore) {
                    max_i = j;
                }
            }

            // Swap the found maximum element with the first element
            Record temp = array[max_i];
            array[max_i] = array[i];
            array[i] = temp;

        }
    }

}
