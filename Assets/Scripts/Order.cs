using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    int food;
    public int Food
    {
        get => food;
        set => food = value;
    }
    int time;
    Table table;
    float elapsedTime;

    public Order(int food, int time, Table table)
    {
        this.food = food;
        this.time = time;
        this.table = table;
    }

    public IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(time);
        table.EndOrderTime();
    }
}