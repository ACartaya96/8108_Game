using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject enemy;
   
    public void Switch()
    {
        
        Debug.Log(enemy.name + " Disabled");
        enemy.GetComponent<EnemyControlSystem>().SwitchState(enemy.GetComponent<EnemyControlSystem>().DisabledState);
    }
    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(enemy.GetComponent<EnemyControlSystem>().switchPos.position, player.transform.position) <= 3.0f && enemy.GetComponent<EnemyControlSystem>()._state != enemy.GetComponent<EnemyControlSystem>().DisabledState)
            Switch();
    }
}
