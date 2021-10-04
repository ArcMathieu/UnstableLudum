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
    public Transform teleportation;
    public RecipeManager recipeManager;
    public Animator animator;
    public float errorTime = 5f;
    public int maxErrors = 2;
    public Animator handAnimator = null;

    private List<WrongElement> wrongElements;

    private void Start() {
        wrongElements = new List<WrongElement>();
    }

    private void Retrieve(Element element) {
        animator.SetTrigger("CloseHand");
        for (int i = 0; i < wrongElements.Count; i++) {
            if (wrongElements[i].element.opposite.Name == element.Name) {
                recipeManager.listElementBeenGet.Remove(wrongElements[i].element);
                recipeManager.Refresh();
                CorrectWrongElement(i);
                return;
            }
        }

        if (!recipeManager.FillFirstElement(element)) {
            if (wrongElements.Count + 1 > maxErrors) {
                GameManager._instance.Lose();
                return;
            }
            AddWrongElement(element);
        }
    }

    private void AddWrongElement(Element element) {
        wrongElements.Add(new WrongElement(element, StartCoroutine(IWrongElementTimer(errorTime))));
        recipeManager.AddElementBeenGet(element);
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
            element.gameObject.transform.position = teleportation.position;
            element.GetComponent<SpriteRenderer>().sortingLayerID = GameManager._instance.cauldronSortingLayer.sortingLayerID;
            SoundManager.PlaySound(SoundManager.Sound.TeleportationHandNotEndermanAtAll);
            handAnimator.SetTrigger("Close");
            Retrieve(element);
        }
    }
}
