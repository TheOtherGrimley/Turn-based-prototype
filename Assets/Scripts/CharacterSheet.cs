using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour {
    public GameObject healthbar_prefab;
    public GameObject spellButtonPrefab;
    public Transform spellParent;
    private Text healthText;
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public SpellSlots spellSlots;
    public int level = 1;

    private List<GameObject> menuLayers = new List<GameObject>();
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
        foreach(GameObject z in GameObject.FindGameObjectsWithTag("Menu Layer"))
            menuLayers.Add(z);

        resetMenus();
        
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
                resetMenus();
                break;
            }
        }
    }

    private void resetMenus()
    {
        for (int i = 0; i < menuLayers.Count; i++)
            if (i == 0)
                menuLayers[i].SetActive(true);
            else
                menuLayers[i].SetActive(false);
    }

    private void loadSpells()
    {
        JsonData s = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/scripts/combat/spells.json"));
        List<Spell> spellList = new List<Spell>();
        //Transform spellParent = null;
        List<int> existingLevels = new List<int>();

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Menu Layer"))
        {
            if(g.name == "Spells")
            {
                //spellParent = g.transform;
            }
        }

        for (int i = 0; i < s["spells"].Count; i++)
        {
            Spell temp = new Spell();
            temp.Id = (int)s["spells"][i][0];
            temp.name = (string)s["spells"][i][1];
            temp.lowerBound = (int)s["spells"][i][2];
            temp.upperBound = (int)s["spells"][i][3];
            temp.level = (int)s["spells"][i][4];
            if (!existingLevels.Contains(temp.level))
            {
                GameObject empty = Instantiate(new GameObject(), spellParent);
                empty.name = temp.level.ToString();
                existingLevels.Add(temp.level);
            }
            GameObject SpellButton = Instantiate(spellButtonPrefab, spellParent.GetChild(1).transform);
            SpellButton.name = temp.name;
            SpellButton.GetComponent<SpellButton>().spellId = temp.Id;
            
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
        public int level;
    }
}
