using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour {
    public int hitPoints;
    private SpriteRenderer sprite;
    private GoalManager goalManager;
    public Sprite sprite2;
    public Sprite sprite3;

    private void Start() {
        goalManager = FindObjectOfType<GoalManager>();
        sprite = GetComponent<SpriteRenderer>();        
    }

    private void Update()
    {
        if (hitPoints <= 0)
        {
            if (goalManager != null) {
                goalManager.CompareGoal(this.gameObject.tag);
                goalManager.UpdateGoals();
            }
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        sprite = GetComponent<SpriteRenderer>();
        MakeLighter(hitPoints);
    }

    void MakeLighter(int hitPoints)
    {
        switch (hitPoints) {
            case 2:
                sprite.sprite = sprite2;
                break;
            case 1:
                sprite.sprite = sprite3;
                break;
            default:
                break;
        }
        //Color color = sprite.color;
        ////прозрачность
        //float newAlpha = color.a * .5f;
        //sprite.color = new Color(color.r, color.g, color.b, newAlpha);
    }
}
