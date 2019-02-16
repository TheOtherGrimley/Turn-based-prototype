using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellButton : MonoBehaviour {
    public int spellId;

    public void cast()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSheet>().castSpell(spellId);
    }

}
