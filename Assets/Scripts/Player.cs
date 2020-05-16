using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

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
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        if (Input.GetButtonDown("Jump"))
        {
            if (!carrying)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, foodLayer))
                {
                    //anim.SetTrigger("PickUp");
                    pickedGameObject = hit.collider.gameObject;
                    pickedGameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    carrying = true;
                }
            }
            else
            {
                anim.SetTrigger("Throw");
                if (Physics.Raycast(transform.position, transform.forward, out hit, 1000, tablesLayer))
                {
                    Vector3 table = hit.transform.position + new Vector3(0, desfaseY, 0);
                    StartCoroutine(GameManager.instance.MoveToPoint(pickedGameObject, table, throwForce));
                    hit.collider.gameObject.GetComponent<Table>().ServeFood(pickedGameObject.GetComponent<Food>().Points);
                    carrying = false;
                    pickedGameObject = null;
                }
                else
                {
                    pickedGameObject.GetComponent<Rigidbody>().velocity = (transform.forward + Vector3.up) * throwForce;
                    StartCoroutine(GameManager.instance.FadeOut(pickedGameObject));
                    pickedGameObject = null;
                    carrying = false;
                }
            }
        }

        if (carrying)
        {
            pickedGameObject.transform.position = pickUpPosition.position;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Axis.normalized.magnitude * Vector3.forward * moveSpeed * Time.deltaTime);
        if (Axis != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Axis.normalized);
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("Walking", Mathf.Abs(Axis.normalized.magnitude));
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

    Vector3 Axis
    {
        get => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}

