using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{

    public float sensibility;
    public float maxAngle;
    public float rDamping;

    float StartRot;
    Vector3 rot;
    float antRotZ;
    float rotZ;
    float rotAccelerate;
    float angle;

    enum State { flat, sloping }
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        StartRot = transform.localEulerAngles.z;
        Debug.Log("hrhr" + StartRot);
        antRotZ = StartRot;
        currentState = State.flat;
    }

    // Update is called once per frame
    void Update()
    {
        float acceleration = Input.acceleration.x;
        rotZ = transform.localEulerAngles.z;
        switch (currentState)
        {
            case State.flat:
                MoveToFlat(acceleration);
                break;
            case State.sloping:
                rotAccelerate = StartRot + acceleration * sensibility;
                MoveToSloping(acceleration);
                break;
            default:
                break;
        }

    }

    public void MoveToFlat(float acceleration)
    {
        if (acceleration >= -0.1 && acceleration <= 0.1)
        {
            angle = Mathf.Clamp(Mathf.Lerp(rotZ, StartRot, rDamping * Time.deltaTime), StartRot - maxAngle, StartRot + maxAngle);
            rot = new Vector3(0, 0, angle);
            transform.localEulerAngles = rot;
        }
        else
        {
            rotZ = StartRot;
            currentState = State.sloping;
        }
    }
    public void MoveToSloping(float acceleration)
    {
        Debug.Log("juju" + rotAccelerate);

        if (acceleration >= -0.1 && acceleration <= 0.1)
        {
            currentState = State.flat;
        }
        else
        {

            angle = Mathf.Clamp(Mathf.Lerp(rotZ, rotAccelerate, rDamping * Time.deltaTime), StartRot - maxAngle, StartRot + maxAngle);
            rot = new Vector3(0, 0, angle);
            transform.localEulerAngles = rot;
        }
    }
    private float AcuteAngle(float angle)
    {
        if (angle < 0)
        {
            return angle + 360;
        }
        return angle;
    }
}
