using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
        float currentTime = time;
        table.timer.color = new Color(1f, 1f, 1f);
        while (currentTime > time * 0.4)
        {
            table.timer.fillAmount = 0.7f * currentTime / time;
            currentTime -= Time.deltaTime;
            yield return null;
        }
        table.timer.color = new Color(0.88f, 0.29f, 0.29f);
        while (currentTime > 0.01f)
        {
            table.timer.fillAmount = 0.7f * currentTime / time;
            currentTime -= Time.deltaTime;
            yield return null;
        }

        table.EndOrderTime();
    }
}