using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 StartPos;
    Vector2 pos;
    Vector3 touchPosition;

    bool moveAllowed;
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
            pos = rb.position;

            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition)) moveAllowed = true;
                    break;
                case TouchPhase.Moved:
                    if (moveAllowed) pos.x = Mathf.Clamp(touchPosition.x, StartPos.x - gap, StartPos.x + gap);
                    else pos.x = Mathf.Clamp(pos.x, StartPos.x - gap, StartPos.x + gap);
                    break;
                case TouchPhase.Ended:
                    moveAllowed = false;
                    rb.velocity = Vector2.zero;
                    break;
                default:
                    break;
            }

            rb.position = pos;

        }
    }
}
