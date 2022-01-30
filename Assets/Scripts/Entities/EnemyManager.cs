using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int enemyCount;
    private static EnemyManager _instance;

    public static EnemyManager Instance { get { return _instance; } }

    public int maxEnemies;
    public float maxEnemyDistance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

}
