using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    // Player 1
    public int p1_health = 100;
    public int p1_damage = 10;
    public int p1_defense = 5;

    // Player 2
    public int p2_health = 100;
    public int p2_damage = 10;
    public int p2_defense = 5;

    // Attack function
    void Attack(GameObject attacker, GameObject defender)
    {
        int damage = attacker.GetComponent<CombatController>().p1_damage - defender.GetComponent<CombatController>().p2_defense;
        if (damage < 0)
        {
            damage = 0;
        }
        defender.GetComponent<CombatController>().p2_health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        // Player 1 attacks
        if (Input.GetButtonDown("P1_Attack"))
        {
            Attack(gameObject, GameObject.FindWithTag("Player2"));
        }

        // Player 2 attacks
        if (Input.GetButtonDown("P2_Attack"))
        {
            Attack(GameObject.FindWithTag("Player2"), gameObject);
        }
    }
}

