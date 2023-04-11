using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible {
    public string name;

    public int score;

    public int amplitude;

    Color color;

    public Collectible(string name, int amplitude, Color color) {
        this.name = name;
        this.amplitude = amplitude;
        this.color = color;
    }

    public void Heal() {
        PlayerManager.playerManager.player.GetComponent<CharacterStats>().Heal(amplitude);
    }

    public void Speed() {
        PlayerManager.playerManager.player.GetComponent<CharacterStats>().Speed(amplitude);
    }
}
