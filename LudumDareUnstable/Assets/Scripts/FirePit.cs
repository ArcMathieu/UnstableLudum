using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePit : MonoBehaviour
{
    public int fireLevel;
    public int fireToAdd = 10;
    public ParticleSystem fireParticles;
    public int fireLost;
    public float fireTimer;
    public Transform spiderParent;
    private float timer;
    public SpriteRenderer pentagramSprite = null;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            timer = fireTimer;
            if (fireLevel >= 0 && fireLevel <= 100) {
                AddFire(-fireLost);
            }
        }

        if (Input.touchCount > 0) {
            Vector3 touchPosition;
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition)) AddFire(fireToAdd);
        } else {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider == GetComponent<Collider2D>()) AddFire(fireToAdd);
            }
        }
        CheckLosing();
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
        ParticleSystem.EmissionModule emission = fireParticles.emission;
        emission.rateOverTime = fireLevel;
        pentagramSprite.color = Color.Lerp(Color.black, Color.red, fireLevel/100f);
        fireLevel += fireAdded;

        spiderParent.localScale = new Vector3(1, Mathf.Clamp01(1f - fireLevel/100f), 1);

        if (fireAdded > 0) {
            animator.SetTrigger("Blow");
        }
    }
}