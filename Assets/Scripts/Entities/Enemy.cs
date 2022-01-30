using UnityEngine;

public class Enemy : Damageable {

    void Start()
    {
        EnemyManager.enemyCount++;
    }

    void Update()
    {
        Vector3 playerPos = PlayerMotor.player.position;
        Vector3 spawnPos = transform.position;
        playerPos.y = 0;
        spawnPos.y = 0;
        float distance = Vector3.Distance(spawnPos, playerPos);
        if (distance >= EnemyManager.Instance.maxEnemyDistance)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
        EnemyManager.enemyCount--;
    }

}
