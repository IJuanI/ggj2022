using UnityEngine;

public class Player : Damageable {
    
    protected override void Damage(float amount) {
        base.Damage(amount);
        HealthBar.instance.UpdateLife(health);
    }
}