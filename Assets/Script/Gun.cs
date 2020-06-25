using UnityEngine.UI;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Gun : MonoBehaviour
{
    public float damage = 1f;
    public float range = 10f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioClip fire;
    public float fireRate = 0.5f;

    public UnityEngine.UI.Text scoreText;
    int score = 0;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        void Shoot()
        {
            AudioSource.PlayClipAtPoint(fire, transform.position);
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, 
                fpsCam.transform.forward, out hit, range))
            {
            
                Target target = hit.transform.GetComponent<Target>();
                if (target != null && target.transform.tag == "Enemy")
                {
                    score = score + 10;
                    scoreText.text = score.ToString();
                    target.TakeDamage(damage);
                }

                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 0.1f);
            }
        }
    }
}
