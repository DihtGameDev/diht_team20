using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAttacking : MonoBehaviour
{
	public Transform attackPose;
	public float attackRange;
	public LayerMask whatIsEnemy;
	public int damage;

	Animator anim;

	double timeSinceLastAttack = 0;
	bool attacked = false;
    // Start is called before the first frame update
    void Start() {
    	anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update() {
	    if (TimeToAttack()) {
	    	attacked = false;
	    	anim.SetInteger("state", 1);
			timeSinceLastAttack = 0;
	    } else if (timeSinceLastAttack >= 0.7 && !attacked) {
	    	Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, whatIsEnemy);
			for (int i = 0; i < enemiesToDamage.Length; i++) {
				if (anim.GetInteger("state") == 1) {
					enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
				}
			}
			attacked = true;
			timeSinceLastAttack += Time.deltaTime;
	    } else {
	    	timeSinceLastAttack += Time.deltaTime;
	    }
	}

    bool TimeToAttack() {
    	if (timeSinceLastAttack >= 1.0) {
    		anim.SetInteger("state", 2);
    		return Random.Range(1, 1001) < 4;
    	}
    	return false;
    }

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
