using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public static Transform player;

    void Awake() {
        player = transform;
    }
}