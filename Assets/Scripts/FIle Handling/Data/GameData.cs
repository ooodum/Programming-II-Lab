using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public int score;
    public Vector3 pos;

    public GameData() {
        this.score = 0;
        this.pos = Vector3.up * 1.89f;
    }
    public GameData(int score, Vector3 pos) {
        this.score = score;
        this.pos = pos;
    }
}
