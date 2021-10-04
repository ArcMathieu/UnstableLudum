using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour {
    [SerializeField]
    List<Element> listElementToGet = new List<Element>();
    public List<Element> listElementBeenGet = new List<Element>();
    public int maxElementBeenGet;
    public List<Image> elementBeenGetUI = new List<Image>();
    //public Image firstElementUI, secondElementUI;
    public SpriteRenderer firstElementUI, secondElementUI;
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
            int randomRange = Random.Range(0, GameManager._instance.elementArray.Length);
            listElementToGet.Add(GameManager._instance.elementArray[randomRange]);
        }
    }

    public void Refresh() {
        firstElementUI.sprite = listElementToGet[0].sprite.sprite;
        firstElementUI.color = listElementToGet[0].sprite.color;
        if (listElementToGet.Count > 1) {
            secondElementUI.color = listElementToGet[1].sprite.color;
            secondElementUI.sprite = listElementToGet[1].sprite.sprite;
        } else {
            secondElementUI.color = new Color(0, 0, 0, 0);
        }

        for (int i = 0; i < maxElementBeenGet; i++) {
            if (i < listElementBeenGet.Count) {
                elementBeenGetUI[i].color = listElementBeenGet[i].sprite.color;
                elementBeenGetUI[i].sprite = listElementBeenGet[i].sprite.sprite;
            } else {
                elementBeenGetUI[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    public bool FillFirstElement(Element element) {
        if (element.Name == listElementToGet[0].Name) {
            DeleteFirstElement();
            return true;
        }
        return false;
    }

    public void DeleteFirstElement() {
        AddElementBeenGet(listElementToGet[0]);
        listElementToGet.RemoveAt(0);
        if (listElementToGet.Count == 0) {
            NewPotion();
        }
        Refresh();
    }

    public void NewPotion() {
        int potionSize = Random.Range(7, 13);
        AddRandomElements(potionSize);
        listElementBeenGet.Clear();
    }
    public void AddElementBeenGet(Element element) {
        listElementBeenGet.Add(element);
        Debug.Log("been add" + element);
        if (listElementBeenGet.Count > maxElementBeenGet)
            listElementBeenGet.RemoveAt(0);
        Refresh();
    }
}
