using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cauldron : MonoBehaviour {
    public Transform teleportation;
    public Animator animator;
    public Animator handAnimator = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        Element element = collision.GetComponent<Element>();
        if (element != null) {
            element.gameObject.transform.position = teleportation.position;
            element.GetComponent<SpriteRenderer>().sortingLayerID = GameManager._instance.cauldronSortingLayer.sortingLayerID;
            SoundManager.PlaySound(SoundManager.Sound.TeleportationHandNotEndermanAtAll);
            handAnimator.SetTrigger("Close");
            animator.SetTrigger("CloseHand");
            //Retrieve(element);
        }
    }
}
