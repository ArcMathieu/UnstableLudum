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

    void Update() {
        if (delete) {
            delete = false;
            DeleteFirstElement();
        }
    }

    void AddRandomElements(int number) {
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

    public bool FillFirstElement(Element element) {
        if (element.Name == listElement[0].Name) {
            DeleteFirstElement();
            return true;
        }
        return false;
    }

    public void DeleteFirstElement() {
        listElement.RemoveAt(0);
        if (listElement.Count == 0) {
            NewPotion();
        }
        Refresh();
    }

    public void NewPotion() {
        int potionSize = Random.Range(7, 13);
        AddRandomElements(potionSize);
    }
}
