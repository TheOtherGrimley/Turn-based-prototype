using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheet : CharacterSheet {
    private void Update()
    {
        if (this.currentHealth <= 0)
            Destroy(this.gameObject);
    }
}
