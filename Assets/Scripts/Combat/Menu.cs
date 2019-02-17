using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public static Menu menu;
    public GameObject spellButtonPrefab;
    public GameObject spellSlotButton;
    public Transform spellParent;
    public List<SpellLevel> spellLevels = new List<SpellLevel>();

    private List<GameObject> menuLayers = new List<GameObject>();
    private int[] offsets = { 0, 0, 0, 0, 0, 0, 0 };


    // Use this for initialization
    void Start () {
        if (Menu.menu == null)
            Menu.menu = this;

        generateMenus();
        reset();
    }

    private void generateMenus()
    {
        foreach (GameObject z in GameObject.FindGameObjectsWithTag("Menu Layer"))
            menuLayers.Add(z);
    }

    public void reset()
    {
        for (int i = 0; i < menuLayers.Count; i++)
            if (i == 0)
                menuLayers[i].SetActive(true);
            else
                menuLayers[i].SetActive(false);

        foreach (SpellLevel s in spellLevels)
        {
            s.slotButton.SetActive(true);
            foreach (GameObject g in s.groupSpells)
            {
                g.SetActive(false);
            }
        }
    }

    public void createSpellLevel(int level)
    {
        SpellLevel temp_slot = new SpellLevel();
        GameObject slot = Instantiate(spellSlotButton, spellParent);
        slot.name = level.ToString();
        slot.transform.GetChild(0).GetComponent<Text>().text = "Level " + slot.name + " spells";
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(206, -105 + (30*level));
        slot.GetComponent<SpellButton>().levelSwitch = level;
        temp_slot.level = level;
        temp_slot.slotButton = slot;
        temp_slot.groupSpells = new List<GameObject>();
        spellLevels.Add(temp_slot);
    }

    public GameObject createSpell(string name, int id, int level)
    {
        GameObject SpellButton = Instantiate(Menu.menu.spellButtonPrefab, Menu.menu.spellParent);
        SpellButton.SetActive(false);
        SpellButton.name = name;
        SpellButton.transform.GetChild(0).GetComponent<Text>().text = name;
        SpellButton.GetComponent<SpellButton>().spellId = id;
        SpellButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(206, -75 + 30*offsets[level]);
        offsets[level] += 1;
        return SpellButton;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
