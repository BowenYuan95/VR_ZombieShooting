using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10f;
    float timer = 3;
    Animator anim;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

        void Die ()
        {

            anim = this.GetComponent<Animator>();
            anim.SetBool("death", true);
         }
    }

    private void Update()
    {
        anim = this.GetComponent<Animator>();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        timer -= Time.deltaTime;

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.fallingback")
            && !anim.IsInTransition(0))
        {
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
