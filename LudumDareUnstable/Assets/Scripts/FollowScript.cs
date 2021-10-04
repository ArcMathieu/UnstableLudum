using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
    public Transform objectToFollow;
    private Vector2 delta = Vector2.zero;

    void Start() {
        delta = objectToFollow.position - transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = objectToFollow.position - (Vector3)delta;
    }
}
