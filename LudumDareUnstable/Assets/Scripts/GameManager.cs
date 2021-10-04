using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;

    public UIManager ui;

    public GameObject firePanel;
    public GameObject lightPanel;
    public Coroutine blink;

    public bool running;
    public int waitTime;

    private void Awake() {
        if (GameManager._instance == null) {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        ui.DefeatScreenToggle(false);
    }

    #region Blink

    public void StartBlink(float time, float numberOfBlinks) {
        if (blink != null) {
            StopCoroutine(blink);
        }
        blink = StartCoroutine(Blink(time, numberOfBlinks));
    }

    public IEnumerator Blink(float time, float numberOfBlinks) {
        while (numberOfBlinks > 0) {
            numberOfBlinks--;
            yield return new WaitForSeconds(time);
            lightPanel.SetActive(!lightPanel.activeSelf);
        }
        if (lightPanel.activeSelf) {
            lightPanel.SetActive(false);
        }
    }

    #endregion

    public void Restart() {
        Time.timeScale = 1f;
        ui.DefeatScreenToggle(false);
    }

    public void Lose() {
        Time.timeScale = 0f;
        ui.DefeatScreenToggle(false);
    }
}
