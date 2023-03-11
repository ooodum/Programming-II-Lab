using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible
{
    public string name;

    public int score;

    public int hpAmount;

    public Collectible(string name, int hpAmount) {
        this.name = name;
        this.hpAmount = hpAmount;
    }

    public void Heal() {
        PlayerManager.playerManager.player.GetComponent<CharacterStats>().Heal(hpAmount);
    }
}
