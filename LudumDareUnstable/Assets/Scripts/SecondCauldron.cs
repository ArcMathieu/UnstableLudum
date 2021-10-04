using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCauldron : MonoBehaviour {
    public ParticleSystem bubblesBoum;

    private void OnTriggerEnter2D(Collider2D collision) {
        Element element = collision.GetComponent<Element>();
        if (element != null) {
            bubblesBoum.Play();
            SoundManager.PlaySound(SoundManager.Sound.ItemFallingInCauldron);
            element.Retrieve();
        }
    }
}
