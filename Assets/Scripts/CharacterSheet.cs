using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour {
    public GameObject healthbar_prefab;
    private Text healthText;
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public SpellSlots spellSlots;
    public int level = 1;

    private List<Spell> spells;

    private void Start()
    {
        spellSlots = new SpellSlots();
        spellSlots.fillAllSlots();
        currentHealth = maxHealth;
        GameObject g = Instantiate(healthbar_prefab, GameObject.FindGameObjectWithTag("Health Parent").transform);
        healthText = g.GetComponent<Text>();
        healthText.text = currentHealth.ToString();
        Debug.Log(File.Exists(Application.dataPath + "/scripts/combat/spells.json"));
        loadSpells();
    }

    public void levelUp()
    {
        level++;
        currentHealth = maxHealth;
    }

    public void castSpell(int spellId)
    {
        foreach(Spell s in spells)
        {
            if(s.Id == spellId)
            {
                int dmg = Random.Range(s.lowerBound, s.upperBound);
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemySheet>().currentHealth -= dmg;
                Debug.Log("Player did " + dmg + " against enemy");
                break;
            }
        }
    }

    private void loadSpells()
    {
        JsonData s = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/scripts/combat/spells.json"));
        List<Spell> spellList = new List<Spell>();

        for (int i = 0; i < s["spells"].Count; i++)
        {
            Spell temp = new Spell();
            temp.Id = (int)s["spells"][i][0];
            temp.name = (string)s["spells"][i][1];
            temp.lowerBound = (int)s["spells"][i][2];
            temp.upperBound = (int)s["spells"][i][3];
            spellList.Add(temp);
        }
        spells = spellList;
    }

    struct Spell
    {
        public int Id;
        public string name;
        public int lowerBound;
        public int upperBound;
    }
}
