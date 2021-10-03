using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Element : MonoBehaviour {
    public float fireStability = 1f;
    public Element opposite = null;
    public float speed;
    public Transform bounds;

    protected string _name = "";
    private Rigidbody2D _rb = null;

    #region Properties
    string Name {
        get { return _name; } set { _name = value; }
    }
    #endregion

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0f, -speed);
        OnStart();
    }

    private void Update() {
        if (transform.position.y < bounds.position.y) {
            Delete();
        }
        OnUpdate();
    }

    public void Delete() {
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        OnDelete();
        Destroy(gameObject);
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnDelete() { }
}
