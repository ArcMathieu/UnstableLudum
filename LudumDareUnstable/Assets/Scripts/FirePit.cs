using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePit : MonoBehaviour
{
    public int fireLevel;
    public int fireLost;
    public float fireTimer;
    private float timer;
    private RectTransform firePanel;
    public Text fireLevelText;

    private void Start()
    {
        firePanel = GameManager._instance.firePanel.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            timer = fireTimer;
            if (fireLevel >= 0 && fireLevel <= 100)
            {
                AddFire(-fireLost);
                UpdateFireLevelUI();
            }
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

    public void UpdateFireLevelUI()
    {
        firePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fireLevel * 3);
        firePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fireLevel * 2.5f);
        FireLevelTextUI();
    }

    public void FireLevelTextUI()
    {
        fireLevelText.text = fireLevel.ToString();
        fireLevelText.color = new Color(fireLevel * 0.0255f, fireLevelText.color.g, fireLevelText.color.b);
    }
}