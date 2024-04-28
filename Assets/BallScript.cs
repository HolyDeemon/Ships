using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject WaterParticles;
    public GameObject ExplosionParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            var expl = Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
            expl.GetComponent<ParticleSystem>().Play();
            collision.transform.parent.GetComponent<ShipScript>().HEHE();//isDead = true;

            Destroy(gameObject);
            return;
        }

        var water = Instantiate(WaterParticles, transform.position, Quaternion.identity);
        water.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        return;
    }

}
