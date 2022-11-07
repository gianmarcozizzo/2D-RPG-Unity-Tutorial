using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage; // PART 22 dice anche come fare per infliggere 0 a quelli che non vuoi uccidere

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        if(other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger) // AND aggiunto dai commenti (e poi nel video) perchè l'hit faceva danno doppio (la Log ha due collider!)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger; // aggiunto PART 19 e spostato in PART 20
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                //enemy.isKinematic = false;
                if(other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }
                
               //StartCoroutine(KnockCo(hit)); RIMOSSO PART 20
            }
        }
    }

}
