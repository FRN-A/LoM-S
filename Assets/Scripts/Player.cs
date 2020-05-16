using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Platform2DUtils.GameplaySystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField, Range(0.1f, 10f)]
    float maxDistance;
    [SerializeField]
    LayerMask foodLayer;
    [SerializeField]
    LayerMask tablesLayer;
    RaycastHit hit;
    [SerializeField]
    Transform pickUpPosition;
    GameObject pickedGameObject;
    bool carrying = false;
    [SerializeField, Range(0.1f, 15f)]
    float throwForce;
    [SerializeField, Range(0f, 1f)]
    float desfaseY;
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("PickUp")) { 
            if (!carrying)
            {
                pickedGameObject = GameplaySystem.PickUp(this.gameObject, maxDistance, foodLayer, pickUpPosition.position);
                carrying = pickedGameObject != null;
            }
            else
            {
                SoundManager.instance.ThrowFood();
                anim.SetTrigger("Throw");
                if (Physics.Raycast(transform.position, transform.forward, out hit, 1000, tablesLayer))
                {
                    Vector3 table = hit.transform.position + new Vector3(0, desfaseY, 0);
                    StartCoroutine(GameManager.instance.MoveToPoint(pickedGameObject, table, throwForce));
                    hit.collider.gameObject.GetComponent<Table>().ServeFood(pickedGameObject.GetComponent<Food>().Points);
                    carrying = false;
                    pickedGameObject.transform.parent = null;
                    pickedGameObject = null;
                }
                else
                {
                    GameplaySystem.Throw(transform, pickedGameObject, throwForce);
                    StartCoroutine(GameManager.instance.FadeOut(pickedGameObject));
                    carrying = false;
                    pickedGameObject = null;
                }
            }
        }
    }

    void FixedUpdate()
    {
        GameplaySystem.MoveTopdown3D(transform, moveSpeed);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Walking", Mathf.Abs(GameplaySystem.Axis3.normalized.magnitude));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            GameManager.instance.ChangeCam();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            GameManager.instance.ChangeCam();
        }
    }
}

