using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cauldron : MonoBehaviour {
    struct WrongElement {
        public Element element;
        public Coroutine routine;

        public WrongElement(Element _element, Coroutine _routine) {
            element = _element;
            routine = _routine;
        }
    }

    public RecipeManager recipe;
    public float errorTime = 5f;
    public int maxErrors = 2;

    private List<WrongElement> wrongElements;

    private void Start() {
        wrongElements = new List<WrongElement>();
    }

    private void Retrieve(Element element) {
        element.Retrieve();
        for (int i = 0; i < wrongElements.Count; i++) {
            if (wrongElements[i].element.opposite.Name == element.Name) {
                CorrectWrongElement(i);
                return;
            }
        }

        if (recipe.FillFirstElement(element)) {
            // Oui
        } else {
            if (wrongElements.Count + 1 > maxErrors) {
                GameManager._instance.Lose();
                return;
            }
            AddWrongElement(element);
        }
    }

    private void AddWrongElement(Element element) {
        wrongElements.Add(new WrongElement(element, StartCoroutine(IWrongElementTimer(errorTime))));
    }

    private void CorrectWrongElement(int index) {
        StopCoroutine(wrongElements[index].routine);
        wrongElements[index].element.Delete();
        wrongElements.RemoveAt(index);
    }

    private IEnumerator IWrongElementTimer(float time) {
        yield return new WaitForSeconds(time);
        GameManager._instance.Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Element element = collision.GetComponent<Element>();
        if (element != null) {
            Retrieve(element);
        }
    }
}
