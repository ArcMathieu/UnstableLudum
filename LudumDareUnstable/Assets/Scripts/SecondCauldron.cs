using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCauldron : MonoBehaviour { 
        private void OnTriggerEnter2D(Collider2D collision) {
        Element element = collision.GetComponent<Element>();
        if (element != null) {
            element.Retrieve();
        }
    }
}
