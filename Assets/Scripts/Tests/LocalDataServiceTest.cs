using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalDataServiceTest : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI debugText;
    [SerializeField] public TextMeshProUGUI playerIdText;
    [SerializeField] public TextMeshProUGUI playerNameText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI ownedItemIdsText;

    private string testFileName = "player_test.json";
    private LocalDataService dataService;


    private void Start()
    {
        dataService = LocalDataService.Instance;
        if (dataService == null)
        {
            Debug.LogError(message: "LocalDataService instance not found!");
            return;
        }

        RunTests();
    }

    private void RunTests()
    {
        PlayerData testData = new PlayerData
        {
            playerId = Guid.NewGuid().ToString(),
            playerName = "Linh Khung",
            highScore = 1000,
            ownedItemIds = new List<string> { "item1", "item2", "item3" }
        };

        LogMessage(message: "Starting data service tests");
        
        dataService.Save(data: testData, fileName: testFileName);
        Debug.Log("File saved at: " + Application.persistentDataPath + "/player_test.json");
        
        PlayerData loadedData = dataService.Load<PlayerData>(fileName: testFileName);
       
        Debug.Log("Test id: " + testData.playerId + " vs " + loadedData.playerId);
        Debug.Log("Test name: " + testData.playerName + " vs " + loadedData.playerName);
        Debug.Log("Test score: " + testData.highScore + " vs " + loadedData.highScore);
        Debug.Log("Test items: " + testData.ownedItemIds.Count + " vs " + loadedData.ownedItemIds.Count);
        bool testPassed = 
            loadedData.playerId.Equals(testData.playerId) && 
            loadedData.playerName == testData.playerName &&
            loadedData.highScore == testData.highScore &&
            loadedData.ownedItemIds.Count == testData.ownedItemIds.Count;

        if (testPassed)
        {
            LogMessage(message: "TEST PASSED: Data loaded successfully!");
            if (playerIdText != null) playerIdText.text = loadedData.playerId;
            if (playerNameText != null) playerNameText.text = loadedData.playerName;
            if (highScoreText  != null) highScoreText.text  = loadedData.highScore.ToString();
            if (ownedItemIdsText != null) ownedItemIdsText.text = string.Join(", ", loadedData.ownedItemIds);
        }
        else
        {
            LogMessage(message: "TEST FAILED: Data mismatch!");
        }
    }
    
    private void LogMessage(string message)
    {
        Debug.Log(message: message);
        if (debugText != null)
        {
            debugText.text += message + "\n";
        }
    }
}
