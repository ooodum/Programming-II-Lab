using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileHandler {
    private string dataDir = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCode = "ilikechicken";

    /// <summary>
    /// Creates a file handler
    /// </summary>
    /// <param name="path">The directory path for the file</param>
    /// <param name="name">The file name</param>
    public FileHandler(string path, string name, bool useEncryption) {
        this.dataDir = path;
        this.dataFileName = name;
        this.useEncryption = useEncryption;
    }
    public GameData LoadGame() {
        string fullPath = Path.Combine(dataDir, dataFileName);
        GameData data = null;
        if (File.Exists(fullPath)) {
            try {
                string loadedData = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        loadedData = reader.ReadToEnd();
                    }
                }
                if (useEncryption) {
                    loadedData = EncryptData(loadedData);
                }
                data = JsonUtility.FromJson<GameData>(loadedData);
            } catch (Exception e) {
                Debug.LogError("Could not load data at: " + fullPath + "\n" + e);
            }
        } 
        return data;
    }
    public void SaveGame(GameData data) {
        string fullPath = Path.Combine(dataDir, dataFileName);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string storedData = JsonUtility.ToJson(data, true);

            if (useEncryption) {
                storedData = EncryptData(storedData);
            }
            
            //Use the using() function to ensure that the FileStream connection is closed once we are finished with FileStream.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using(StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(storedData);
                }
            }
        } catch(Exception e) {
            Debug.LogError("Could not save to: " + fullPath + "\n" + e);
        }
    }

    private string EncryptData(string data) {
        string modifiedData = "";

        for(int i = 0; i < data.Length; i++) {
            modifiedData += (char)(data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }

        return modifiedData;
    }
}
