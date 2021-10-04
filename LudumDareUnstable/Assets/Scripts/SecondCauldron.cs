using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SecondCauldron : MonoBehaviour {
    struct WrongElement {
        public Element element;
        public Coroutine routine;

        public WrongElement(Element _element, Coroutine _routine) {
            element = _element;
            routine = _routine;
        }
    }

    public RecipeManager recipeManager;
    public ParticleSystem bubblesBoum;

    [Header("Error")]
    public SpriteRenderer liquidRender;
    public List<Light2D> lights;
    public Color errorColor;
    private Color baseColor;
    public float errorTime = 5f;
    public int maxErrors = 2;

    private List<WrongElement> wrongElements;

    private void Start() {
        wrongElements = new List<WrongElement>();
        baseColor = liquidRender.color;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Element element = collision.GetComponent<Element>();
        if (element != null) {
            bubblesBoum.Play();
            SoundManager.PlaySound(SoundManager.Sound.ItemFallingInCauldron);
            Retrieve(element);
            element.Retrieve();
        }
    }

    public void Retrieve(Element element) {
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
            liquidRender.color = errorColor;
            for (int i = 0; i < lights.Count; i++) {
                lights[i].color = errorColor;
            }
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

        if (wrongElements.Count <= 0) {
            liquidRender.color = baseColor;
            for (int i = 0; i < lights.Count; i++) {
                lights[i].color = baseColor;
            }
        }
    }

    private IEnumerator IWrongElementTimer(float time) {
        yield return new WaitForSeconds(time);
        GameManager._instance.Lose();
    }
}
