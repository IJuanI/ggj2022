using System.Collections.Generic;
using UnityEngine;

enum CollType { Box, Circle }

[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour {

    public Platformer.Mechanics.PatrolPath path;

    [Header("Spawn Config")]
    public float spawnRate = 1f;
    public List<EntitySpawn> entities;
    public Transform enemiesParent;

    [Header("Gizmos config")]
    public bool showGizmos = true;
    public Color gizmosColor = new Color(.2f, .4f, .75f, .1f);

    float spawnDelta, spawnCounter;
    float rateSum;
    Collider2D coll;
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

    void Start() { Initialize(true); }

    void Initialize(bool force = false) {
        if (coll == null && !force)
            coll = GetComponent<Collider2D>();
        if (collType == null && !force)
            collType = coll is BoxCollider2D ? CollType.Box : CollType.Circle;
    }

    void Update()
    {
        spawnCounter += Time.deltaTime;
        while (spawnCounter > spawnDelta)
        {
            spawnCounter -= spawnDelta;
            Spawn();
        }
    }

    void Spawn() {
        var enemyPrefab = PickEntity();
        var spawnPoint = RandomPointInCollider();
        if (spawnPoint == null) return;
        var enemy = Instantiate(enemyPrefab, spawnPoint.Value, enemyPrefab.transform.rotation, enemiesParent);
        if (enemy.GetComponent<Platformer.Mechanics.EnemyController>() != null)
            enemy.GetComponent<Platformer.Mechanics.EnemyController>().path = path;
    }

    Vector2? RandomPointInCollider()
    {
        switch (collType)
        {
            case CollType.Box:
                return new Vector2(
                    Random.Range(coll.bounds.min.x, coll.bounds.max.x),
                    Random.Range(coll.bounds.min.y, coll.bounds.max.y)
                );
            case CollType.Circle:
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
            case CollType.Circle:
                Gizmos.DrawSphere(coll.bounds.center, coll.bounds.extents.x);
                break;
            default:
                throw new System.NotImplementedException();
        }

    }

}