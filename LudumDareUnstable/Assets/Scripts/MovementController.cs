using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 StartPos;
    Vector2 pos;
    Vector3 touchPosition;
    Vector3 direction;

    public int speed;
    public float gap;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartPos = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        Moves();
    }

    public void Moves()
    {
        if (Input.touchCount > 0)
        {
            pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, StartPos.x - gap, StartPos.x + gap);

            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);

            rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, 0);
            transform.position = pos;

            if (touch.phase == TouchPhase.Ended) rb.velocity = Vector2.zero;
        }
    }
}
