using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank)
        {
            bank.Deposit(goldReward);
        }
    }
    public void StealGold()
    {
        if (bank)
        {
            bank.Withdraw(goldPenalty);
            //commen
        }
    }
}
