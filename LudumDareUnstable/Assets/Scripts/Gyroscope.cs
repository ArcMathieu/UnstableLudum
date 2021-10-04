using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public float speed;
    Vector2 StartPos;
    Vector2 pos;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = GetComponent<Rigidbody2D>().position;

    }
    // Update is called once per frame
    void Update()
    {

        rot = new Quaternion(0, 0, 0, Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, StartPos.x - 4, StartPos.x + 4));
        float moveHorizontal = Input.acceleration.x;
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (pos.x <= StartPos.x - 10 || pos.x >= StartPos.x + 10) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        else GetComponent<Rigidbody2D>().AddForce(movement * speed);

         GetComponent<Rigidbody2D>().position = pos;
    }
}
