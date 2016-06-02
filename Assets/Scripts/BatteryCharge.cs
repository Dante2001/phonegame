using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryCharge : MonoBehaviour {

    public Slider batteryBar;
    private float maxBattery = 100f;
    private float minBattery = -50f;
    private float currentBattery = 100f;
    private float rechargeRatePerSecond = 10f;
    private float slowRechargeRatePerSecond = 7f;
    private bool isSlowRecharge = false;
    private float slowToFastBreakingPoint = 40f;
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isAI)
            playerBatteryUpdate();
        else
            aiBatteryUpdate();
	}

    public void InitializeBattery(float recharge, float sRecharge, float bp)
    {
        rechargeRatePerSecond = recharge;
        slowRechargeRatePerSecond = sRecharge;
        slowToFastBreakingPoint = bp;
    }

    private void playerBatteryUpdate()
    {
        if (currentBattery < maxBattery)
        {
            if (isSlowRecharge)
            {
                currentBattery += slowRechargeRatePerSecond * Time.deltaTime;
                if (currentBattery >= slowRechargeRatePerSecond)
                    isSlowRecharge = false;
            }
            else
            {
                currentBattery += rechargeRatePerSecond * Time.deltaTime;
            }
            if (currentBattery > maxBattery)
                currentBattery = maxBattery;
        }
        if (batteryBar != null)
            batteryBar.value = currentBattery;
    }

    private void aiBatteryUpdate()
    {
        if (batteryBar != null)
            batteryBar.value = currentBattery;
    }

    public void RegainBattery(float amount)
    {
        currentBattery += amount;
        if (currentBattery > maxBattery)
            currentBattery = maxBattery;
    }
    
    public bool isDead()
    {
        if (currentBattery <= minBattery)
            return true;
        return false;
    }

    public bool CheckAmount(float cost)
    {
        if (currentBattery - minBattery >= cost)
            return true;
        return false;
    }

    public void DrainBattery(float cost)
    {
        currentBattery -= cost;
        if (currentBattery < 0)
            isSlowRecharge = true;
    }

    public void Respawn()
    {
        currentBattery = maxBattery;
        isSlowRecharge = false;
        if (batteryBar != null)
            batteryBar.value = currentBattery;
    }
}
