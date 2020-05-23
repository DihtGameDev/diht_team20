using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantHit : MonoBehaviour
{
	private float timeBtwAttack;
	public float startTimeBtwAttack;

	public Transform attackPose;
	public float attackRange;
	public LayerMask whatIsEnemy;
	public int damage;

	Animator anim;

	double timeSinceLastAttack = 0;
    // Start is called before the first frame update
    void Start() {
    	anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update() {
        if (TimeToAttack()) {
        	anim.SetInteger("state", 2);
			timeSinceLastAttack = 0;
			Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, whatIsEnemy);
			for (int i = 0; i < enemiesToDamage.Length; i++) {
				enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
			}
        } else {
        	timeSinceLastAttack += Time.deltaTime;
        }
    }

    bool TimeToAttack() {
    	if (timeSinceLastAttack >= 0.8) {
    		return Random.Range(1, 1001) < 5;
    	}
    	return false;
    }

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
