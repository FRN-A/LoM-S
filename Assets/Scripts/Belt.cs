using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Food"))
        {
            Debug.Log("Enter");
            collision.gameObject.GetComponent<Rigidbody>().velocity = moveSpeed * Time.deltaTime * Vector3.right;
        }
    }
}
