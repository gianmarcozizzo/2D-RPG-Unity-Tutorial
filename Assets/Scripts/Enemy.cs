using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState // PART 19
{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth; // PART 22
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            //enemy.isKinematic = true;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

}
