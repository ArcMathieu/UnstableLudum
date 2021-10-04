using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Element : MonoBehaviour {
    public float fireStability = 1f;
    public float speed = 1f;
    public Element opposite = null;

    [HideInInspector] public Transform bounds = null;

    [SerializeField] private string _name = "";
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
        if (bounds != null && transform.position.y < bounds.position.y) {
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

    public void Retrieve() {
        OnRetrieve();
        Delete();
    }

    public void ChangeSpeed(float speed) {
        _rb.velocity = new Vector2(0f, -speed);
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnDelete() { }
    protected virtual void OnRetrieve() { }
}
