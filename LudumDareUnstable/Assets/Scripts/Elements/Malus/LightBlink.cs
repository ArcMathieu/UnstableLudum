using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : Element
{
    public float blinkTimer;
    public float numberOfBlinks;

    protected override void OnRetrieve()
    {
        GameManager._instance.StartBlink(blinkTimer, numberOfBlinks);
    }
}