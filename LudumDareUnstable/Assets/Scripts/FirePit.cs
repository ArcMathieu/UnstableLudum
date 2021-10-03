using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{
    public int fireLevel;
    public int fireLost;
    public float fireTimer;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            timer = fireTimer;
            AddFire(-fireLost);
        }
    }

    public void CheckLosing()
    {
        if (fireLevel <= 0)
        {
            GameManager._instance.Lose();
        }
        else if (fireLevel >= 100)
        {
            GameManager._instance.Lose();
        }
    }

    public void AddFire(int fireAdded)
    {
        fireLevel += fireAdded;
    }
}