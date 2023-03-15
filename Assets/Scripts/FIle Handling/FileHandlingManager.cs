using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandlingManager:MonoBehaviour {
    public static FileHandlingManager instance;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private FileHandler fileHandler;
    private void Awake() {
        if (instance == null) instance = this;
    }
    private void Start() {
        this.fileHandler = new FileHandler(Application.persistentDataPath, fileName);
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
        } else {
            print("Loaded score of: " + gameData.score);
        }
    }

    public void SaveGame() {
        fileHandler.SaveGame(gameData);
        Debug.Log("Saved score of: " + gameData.score);
    }
}
