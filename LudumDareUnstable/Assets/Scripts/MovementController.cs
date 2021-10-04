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
        pos = rb.position;

        if (Input.touchCount > 0)
        {

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

        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider == GetComponent<Collider2D>()) moveAllowed = true;
            }
            if (Input.GetMouseButton(0) && moveAllowed)
            {
                if (moveAllowed) pos.x = Mathf.Clamp(mousePosition.x, StartPos.x - gap, StartPos.x + gap);
                else pos.x = Mathf.Clamp(pos.x, StartPos.x - gap, StartPos.x + gap);
            }
            if (Input.GetMouseButtonUp(0))
            {
                moveAllowed = false;
                rb.velocity = Vector2.zero;
            }

        }


        rb.position = pos;

    }
}
