using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackTriggerZone : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trying to damage player");
        Debug.Log("We have hit" + other.gameObject);

        // If we are RED and other is BLUE... -AG
        if((transform.parent.tag == "Player Red" && other.gameObject.tag == "Player Blue") || (transform.parent.tag == "Player Blue" && other.gameObject.tag == "Player Red"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(playerController.attackDamage);
            Debug.Log("My tag is " + gameObject.tag + ". Other tag is " + other.tag + ".");
            Debug.Log(gameObject.name + " taking damage");
        }
    }
}
