using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
	private float timeBtwAttack;
	public float startTimeBtwAttack;

	public Transform attackPose;
	public float attackRange;
	public LayerMask whatIsEnemy;
	public int damage;

	Animator anim;
    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0) {
        	if (Input.GetKey(KeyCode.RightShift)) {
        		anim.SetInteger("state", 1);
        		timeBtwAttack = startTimeBtwAttack;
        		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, whatIsEnemy);
        		for (int i = 0; i < enemiesToDamage.Length; i++) {
        			enemiesToDamage[i].GetComponent<Giant>().TakeDamage(damage);
        		}
        	}
        } else {
        	anim.SetInteger("state", 2);
        	timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
