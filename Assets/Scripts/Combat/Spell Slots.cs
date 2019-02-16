using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlots {
    private int[] _current_slots = { 0, 0, 0, 0, 0, 0, 0 };
    private int[] _max_slots = { 4, 1, 2, 0, 0, 0, 0 };

    public void fillAllSlots()
    {
        _current_slots = _max_slots;
        Debug.Log("Spell slots filled!");
    }

    public void replenishSlot(int slotLevel)
    {
        if(slotLevel > 7 || slotLevel <= 0)
        {
            Debug.LogError("Spell slot outside maximum. Spells should be between levels 1 and 7. You entered " + slotLevel);
        }
        else
        {
            if(_current_slots[slotLevel - 1] >= _max_slots[slotLevel - 1])
            {
                Debug.Log("Spell slot already full");
            }
            else
            {
                _current_slots[slotLevel - 1] += 1;
            }
        }
    }

    public int[] getAllCurrentSlots()
    {
        return _current_slots;
    }

    public int[] getAllMaxSlots()
    {
        return _max_slots;
    }

    public int getCurrentInSlot(int slotLevel)
    {
        int ret = 0;

        if (slotLevel > 7 || slotLevel <= 0)
        {
            Debug.LogError("Spell slot outside maximum. Spells should be between levels 1 and 7. You entered " + slotLevel);
        }
        else
        {
            ret = _current_slots[slotLevel - 1];
        }

        return ret;
    }

    public int getMaxInSlot(int slotLevel)
    {
        int ret = 0;

        if (slotLevel > 7 || slotLevel <= 0)
        {
            Debug.LogError("Spell slot outsid" +
                "e maximum. Spells should be between levels 1 and 7. You entered " + slotLevel);
        }
        else
        {
            ret = _max_slots[slotLevel - 1];
        }

        return ret;
    }

    public void levelUpSlot(int slotLevel)
    {
        if (slotLevel > 7 || slotLevel <= 0)
        {
            Debug.LogError("Spell slot outside maximum. Spells should be between levels 1 and 7. You entered " + slotLevel);
        }
        else
        {
            _max_slots[slotLevel - 1] += 1;
        }
    }
}
