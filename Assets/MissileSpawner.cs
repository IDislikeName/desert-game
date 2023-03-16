using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{

    public LayerMask layermask;
    public Transform target;
    public GameObject missilePrefab;
    public int amount;
    public float spawnInterval, destroyDelay;

    ParticleSystem ps;

    bool isDead;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.Find("Particle").GetComponent<ParticleSystem>();
        timer = spawnInterval;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        if(amount > 0)
        {
            if (timer <= 0)
            {
                SpawnMissile();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            Destroy(gameObject, destroyDelay);
            ps.Stop();
            isDead = true;
        }
    }

    void SpawnMissile()
    {
        timer = spawnInterval;
        amount -= 1;
        var missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        if (target)
        {
            missile.GetComponent<MissileBehavior>().target = target;
        }
    }
}
