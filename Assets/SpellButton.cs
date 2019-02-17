using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellButton : MonoBehaviour {
    public int spellId;
    public int levelSwitch;

    public void cast()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSheet>().castSpell(spellId);
    }

    public void switchSpellLevel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSheet>().SpellLevelMenuChange(levelSwitch);
    }

}
