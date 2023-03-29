using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FileHandlingManager:MonoBehaviour {
    public static FileHandlingManager instance;

    private GameData gameData;
    private FileHandler fileHandler;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    private List<DataPersistence> dataPersistences;
    private void Awake() {
        if (instance == null) instance = this; else print("More than one FileHandlingManager in the scene");
        this.fileHandler = new FileHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistences = FindAllDataPersistences();
        LoadGame();
    }
    private void OnApplicationQuit() {
        SaveGame();
    }
    public void NewGame() {
        gameData = new GameData();
    }

    public void LoadGame() {
        this.gameData = fileHandler.LoadGame();
        if (this.gameData == null) {
            print("No game data found, initializing data");
            NewGame();
        } 
        foreach (DataPersistence dataPersistence in dataPersistences) {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame() {
        fileHandler.SaveGame(gameData);
        Debug.Log("Saved score of: " + gameData.score);
    }

    private List<DataPersistence> FindAllDataPersistences() {
        IEnumerable<DataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<DataPersistence>();
        return new List<DataPersistence>(dataPersistences);
    }
}
