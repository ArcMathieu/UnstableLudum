using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Element : MonoBehaviour
{
    public float fireStability = 1f;
    public float speed = 1f;
    public Element opposite = null;
    public SpriteRenderer sprite;

    [HideInInspector] public Transform bounds = null;

    [SerializeField] private string _name = "";
    private Rigidbody2D _rb = null;

    public Coroutine waitToDropCoroutine;
    public float timeToDrop;

    #region Properties
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        OnStart();
    }

    private void Update()
    {
        if (bounds != null && transform.position.y < bounds.position.y)
        {
            Delete();
        }
        OnUpdate();
    }

    public void Delete()
    {
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        OnDelete();
        Destroy(gameObject);
    }

    public void Retrieve()
    {
        OnRetrieve();
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void ChangeSpeed(float speed)
    {
        _rb.velocity = new Vector2(0f, -speed);
    }
    protected virtual void OnStart()
    {
        if (waitToDropCoroutine != null)
        {
            StopCoroutine(waitToDropCoroutine);
        }
        waitToDropCoroutine = StartCoroutine(iWaitToDrop(timeToDrop));
    }
    public IEnumerator iWaitToDrop(float time)
    {
        yield return new WaitForSeconds(time);
        _rb.velocity = new Vector2(0f, -speed);
    }
    protected virtual void OnUpdate() { }
    protected virtual void OnDelete() { }
    protected virtual void OnRetrieve() { }
}
