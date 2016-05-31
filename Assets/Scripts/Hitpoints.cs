using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hitpoints : MonoBehaviour {

    public Slider HPBar;
    private int maxHitpoints;
    private int currentHitpoints;

    public void InitializeHP(int hp)
    {
        maxHitpoints = hp;
        currentHitpoints = hp;
        if (HPBar != null)
            HPBar.maxValue = maxHitpoints;
            HPBar.value = currentHitpoints;
    }

    public void LoseHP()
    {
        currentHitpoints -= 1;
        SetHP();
    }

    public void GainHP()
    {
        if (currentHitpoints < maxHitpoints)
        {
            currentHitpoints += 1;
            SetHP();
        }
    }

    public int CheckHP()
    {
        return currentHitpoints;
    }

    private void SetHP()
    {
        if (HPBar != null)
            HPBar.value = currentHitpoints;
    }

    public void Respawn()
    {
        currentHitpoints = maxHitpoints;
        SetHP();
    }
}
