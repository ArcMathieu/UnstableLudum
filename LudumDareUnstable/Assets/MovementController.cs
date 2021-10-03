using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 StartPos;
    Vector2 pos;
    float HztVelocity;

    public float speed;
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
        HztVelocity = Input.GetAxisRaw("Horizontal");
        pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, StartPos.x - gap, StartPos.x + gap);
        rb.velocity = new Vector2(HztVelocity * speed * Time.deltaTime, 0);
        transform.position = pos;
    }
}
