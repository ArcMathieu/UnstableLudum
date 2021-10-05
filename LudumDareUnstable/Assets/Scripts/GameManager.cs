using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public Element[] elementArray = new Element[0];

    public UIManager ui;

    public GameObject firePanel;
    public GameObject lightPanel;
    public Coroutine blink;

    public bool running;
    public int waitTime;
    public Score score;

    public SpriteRenderer cauldronSortingLayer;
    public pauseMenu pause;

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
        Time.timeScale = 1f;
        SoundManager.PlayMusic(SoundManager.Sound.BackgroundMusic);
        SoundManager.PlayLoopSound(SoundManager.Sound.Fire);
        SoundManager.PlayLoopSound(SoundManager.Sound.Cauldron);
        GameManager._instance.score.score = 0;
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
        pause.PauseOFF();
        ui.DefeatScreenToggle(false);
    }

    public void Lose() {
        pause.PauseON();
        ui.DefeatScreenToggle(true);
    }
}
