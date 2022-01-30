using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum CollType { Box, Sphere }

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour {

    [Header("Spawn Config")]
    public bool infinite = true;
    [Tooltip("-1 for infinite distance")]
    public float spawnDistance = 10f;
    public float spawnRate = 1f;
    public List<EntitySpawn> entities;
    public Transform enemiesParent;

    [Header("Gizmos config")]
    public bool showGizmos = true;
    public Color gizmosColor = new Color(.2f, .4f, .75f, .1f);

    bool used = false;
    float spawnDelta, spawnCounter;
    float rateSum;
    Collider coll;
    CollType? collType;

    void OnValidate()
    {
        if (entities == null) return;

        // sum all rates of entities into rateSum
        rateSum = 0;
        foreach (EntitySpawn spawn in entities)
            rateSum += spawn.rate;
        spawnDelta = 1f / spawnRate;
    }

    void Start() { OnValidate(); Initialize(true); }

    void Initialize(bool force = false) {
        if (coll == null || force)
            coll = GetComponent<Collider>();
        if (collType == null || force)
            collType = coll is BoxCollider ? CollType.Box : CollType.Sphere;
        if (force) {
            used = false;
        }
    }

    void Update()
    {
        if (!infinite && used) return;
        if (!ShouldSpawn()) return;

        if (infinite) {
            spawnCounter += Time.deltaTime;
            while (spawnCounter > spawnDelta)
            {
                spawnCounter -= spawnDelta;
                Spawn();
            }
        } else {
            for (int i = 0; i < spawnRate; i++)
                Spawn();
        }
    }

    bool ShouldSpawn() {
        if (spawnDistance < 0) return true;
        if (PlayerMotor.player == null) return infinite;

        Vector3 playerPos = PlayerMotor.player.position;
        Vector3 spawnPos = transform.position;
        playerPos.y = 0;
        spawnPos.y = 0;
        float distance = Vector3.Distance(spawnPos, playerPos);
        return distance <= spawnDistance;
    }

    void Spawn() {
        if (EnemyManager.enemyCount >= EnemyManager.Instance.maxEnemies) return;
        var enemyPrefab = PickEntity();
        var randPoint = RandomPointInCollider();
        if (randPoint == null) return;
        Vector3 spawnPoint = new Vector3(randPoint.Value.x, transform.position.y, randPoint.Value.y);
        var enemy = Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation, enemiesParent);
        var obsession = enemy.GetComponent<Obsessed>();
        if (obsession != null)
            obsession.target = PlayerMotor.player;
        var stalker = enemy.GetComponentInChildren<Stalker>();
        if (stalker != null)
            stalker.target = Camera.main.transform;
        used = true;
    }

    Vector2? RandomPointInCollider()
    {
        switch (collType)
        {
            case CollType.Box:
                return new Vector2(
                    Random.Range(coll.bounds.min.x, coll.bounds.max.x),
                    Random.Range(coll.bounds.min.z, coll.bounds.max.z)
                );
            case CollType.Sphere:
                return (Vector2) coll.bounds.center + Random.insideUnitCircle * coll.bounds.extents.magnitude;
        }

        return null;
    }

    Entity PickEntity() {
        float targetRate = Random.Range(0f, rateSum);
        float currRate = 0f;

        foreach (EntitySpawn spawn in entities) {
            currRate += spawn.rate;
            if (currRate >= targetRate)
                return spawn.entity;
        }
        return null;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Initialize();

        // Draw transparent box around collider
        Gizmos.color = gizmosColor;

        switch (collType)
        {
            case CollType.Box:
                Gizmos.DrawCube(coll.bounds.center, coll.bounds.size);
                break;
            case CollType.Sphere:
                Gizmos.DrawSphere(coll.bounds.center, coll.bounds.extents.x);
                break;
            default:
                throw new System.NotImplementedException();
        }

        Gizmos.DrawWireSphere(transform.position, spawnDistance);
    }

}