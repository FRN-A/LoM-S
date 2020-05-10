using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private bool available;
    Order order;

    public bool Available
    {
        get => available;
        set => available = value;
    }

    Vector3 screenPos;

    private void Awake()
    {
        available = true;
        order = null;
    }

    public void NewOrder()
    {
        int food = (int)UnityEngine.Random.Range(1f, 6f);
        int time = food * 3;
        order = new Order(food, time, this);
        available = false;
        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        StartCoroutine(order.TimeToLive());
        Debug.Log($"Nueva orden: quesos:{food}, tiempo:{time}");
    }

    private void OnGUI()
    {
        if(order != null)
        {
            GUI.Label(new Rect(screenPos.x, screenPos.y, 200, 30), $"{order.Food}");
        }
    }

    public void ServeFood()
    {
        if (!available)
        {
            if (order.Food > 1)
            {
                order.Food--;
                Debug.Log("+1 queso");
            }
            else
            {
                EndOrder();
            }
        }
        else
        {
            GameManager.instance.player.Lifes--;
            Debug.Log($"vidas: {GameManager.instance.player.Lifes}");
        }
    }

    void EndOrder()
    {
        order = null;
        available = true;
        Debug.Log("Orden servida");
    }

    public void EndOrderTime()
    {
        order = null;
        available = true;
        GameManager.instance.player.Lifes--;
        Debug.Log("Se acabó el tiempo");
        Debug.Log($"vidas: {GameManager.instance.player.Lifes}");
    }
}
