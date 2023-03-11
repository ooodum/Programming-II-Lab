using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;

    public GameObject player;


    private void Awake() {
        if (playerManager == null) {
            playerManager = this;
        }
    }
}
