using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    public bool canvasActive;
    public void StartLoadingScreen() {
        canvasActive = true;
        StartCoroutine(LoadingTimer());
    }
    private void Update() {
        if (canvasActive && canvas.alpha < 1) {
            canvas.alpha += Time.deltaTime;
        }
    }

    private IEnumerator LoadingTimer() {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }
}
