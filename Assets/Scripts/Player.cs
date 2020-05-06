using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField, Range(0.1f, 10f)]
    float maxDistance;
    [SerializeField]
    LayerMask layerMask;
    RaycastHit hit;
    [SerializeField]
    Transform pickUpPosition;
    GameObject pickedGameObject;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.yellow);
        if (Input.GetButton("Jump"))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
            {
                Debug.Log("hit");
                Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
                pickedGameObject = hit.collider.gameObject;
                pickedGameObject.GetComponent<Rigidbody>().detectCollisions = false;
            }
        }

        if (pickedGameObject)
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

