using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour {
    public SpawnManager spawnManager;
    List<Element> listElement = new List<Element>();
    public Image firstElementUI, secondElementUI;
    public bool delete;
    void Start() {
        NewPotion();
        Refresh();
    }

    // Update is called once per frame
    void Update() {
        if (delete) {
            delete = false;
            DeleteFirstElement();
        }
    }

    void AddRandomElement(int number) {
        for (int i = 0; i < number; i++) {
            int randomRange = Random.Range(0, spawnManager.elementArray.Length);
            listElement.Add(spawnManager.elementArray[randomRange]);
        }
    }

    void Refresh() {

        firstElementUI.sprite = listElement[0].sprite.sprite;
        if (listElement.Count > 1) {
            secondElementUI.color = new Color(1, 1, 1, 1);
            secondElementUI.sprite = listElement[1].sprite.sprite;
        } else {
            secondElementUI.color = new Color(0,0,0,0);
        }
    }

    void DeleteFirstElement() {
        listElement.RemoveAt(0);
        if (listElement.Count == 0) {
            NewPotion();
        }
        Refresh();
    }
    void NewPotion() {
        int potionSize = Random.Range(7, 13);
        AddRandomElement(potionSize);
    }
}
