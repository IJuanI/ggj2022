using UnityEngine;

public class ShadersManager : MonoBehaviour
{
    private static ShadersManager _instance;

    public static ShadersManager Instance { get { return _instance; } }


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

    void Update()
    {
        WaveEffect.Update();
    }

}
