using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
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
    public GameObject errorText;
    public GameObject addToLeaderboardMenu;
    public GameObject playerNameInput;
    public Transform leaderboardListContent;
    void Start()
    {
        recordPosition = 1;
        errorText.gameObject.SetActive(false);
        addToLeaderboardMenu.gameObject.SetActive(false);
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
                createObjects(record.player, record.highscore.ToString());
            }
        } else {
            Debug.Log(getRecords.error);
            errorText.SetActive(true);
        }

    }

    public void SubmitRecord() {
        StartCoroutine(PostRecord());
    }

    IEnumerator PostRecord() {
        int playerHighscore = PlayerPrefs.GetInt("Highscore", 0);
        string playerName = playerNameInput.GetComponent<InputField>().text;
        Record data = new Record(); // Create a new Record object
        data.player = playerName; 
        data.highscore = playerHighscore;
        string jsonData = JsonUtility.ToJson(data); // Parse to JSON from the object
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(jsonData); // Get Raw Data from JSON
        UnityWebRequest postRecord = UnityWebRequest.Post(url, jsonData); // Send Post request to API
        postRecord.uploadHandler = new UploadHandlerRaw(rawData); // Set data to POST
        postRecord.SetRequestHeader("Content-Type", "application/json"); // Set headers
        postRecord.SetRequestHeader("Accept", "application/json");
        postRecord.SetRequestHeader("Authorization", $"Token {token}");
        yield return postRecord.SendWebRequest();

        if (postRecord.responseCode == 201) {
            Debug.Log("Posted!"); 
            createObjects(playerName, playerHighscore.ToString());  
        } else {
            Debug.Log(postRecord.error);
        }

        addToLeaderboardMenu.SetActive(false);
    }
    // Create record object from API
    void createObjects(string playerName, string playerHighscore) {
        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y); // Assign record object's position
        Quaternion q = this.transform.rotation; // Assign record object's rotation
        recordElement.GetComponent<RecordElement>().positionText.GetComponent<TextMeshProUGUI>().text = recordPosition.ToString();
        recordElement.GetComponent<RecordElement>().playerNameText.GetComponent<TextMeshProUGUI>().text = playerName; 
        recordElement.GetComponent<RecordElement>().highscoreText.GetComponent<TextMeshProUGUI>().text = playerHighscore;
        GameObject newRecordObject = Instantiate(recordElement, position, q);
        newRecordObject.transform.SetParent(leaderboardListContent);
        recordPosition++;
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
