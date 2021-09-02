using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints = 0;

    Enemy enemy;

    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            maxHitPoints += difficultyRamp;
        }
    }
}
