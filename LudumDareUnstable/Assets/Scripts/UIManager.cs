using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject defeatScreen;
    public GameObject menuButton;


    public void DefeatScreenToggle(bool state) {
        if (defeatScreen == null) { Debug.LogWarning("No defeatScreen !"); return; }
        defeatScreen.SetActive(state);
        menuButton.SetActive(!state);
    }
}
