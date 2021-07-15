﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public MoneyManager moneyManager;

    public GameObject basicTowerPrefab;
    public int basicTowerCost;

    public int GetTowerCost(GameObject towerPrefab)
    {
        int cost = 0;

        if (towerPrefab == basicTowerPrefab)
        {
            cost = basicTowerCost;
        }

        return cost;
    }

    public void BuyTower(GameObject towerPrefab)
    {
        moneyManager.RemoveMoney(GetTowerCost(towerPrefab));
    }

    public void SellTower(GameObject towerPrefab)
    {
        moneyManager.AddMoney(GetTowerCost(towerPrefab) / 2);
    }

    public bool CanBuyTower(GameObject towerPrefab)
    {
        int cost = GetTowerCost(towerPrefab);

        bool canBuy = false;

        if (moneyManager.GetCurrentMoney() >= cost)
        {
            canBuy = true;
        }

        return canBuy;
    }
}
