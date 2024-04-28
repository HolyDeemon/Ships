using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public LayerMask waterLayer;
    public float SinkSpeed;
    public float ShipHeight;
    public float Speed;
    public bool isDead;

    private Quaternion pseudorotation;
    private float drowgravity = 0;

    // Start is called before the first frame update
    void Start()
    {
        pseudorotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed;
        if (!isDead)
        {
            Swim();
        }
        else
        {
            Dead();
        }
        if(transform.position.x <= -40)
        {
            GameScript.Score -= 2;
            Destroy(gameObject);
        }
        if(transform.position.y <= -100)
        {
            Destroy(gameObject);
        }
    }
    public void HEHE()
    {
        if (!isDead)
        {
            isDead = true;
            GameScript.Score += 1;
        }
    }
    void Swim()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down, Color.blue);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, waterLayer))
        {
            Debug.DrawRay(transform.position, hit.normal, Color.red);
            pseudorotation = Quaternion.Lerp(pseudorotation, Quaternion.LookRotation(hit.normal, Vector3.forward), Time.deltaTime);
            transform.rotation = pseudorotation;
            transform.Rotate(Vector3.right * 90);
            transform.Rotate(Vector3.up * 180);
            transform.position = Vector3.Lerp(transform.position, hit.point + Vector3.up * ShipHeight, SinkSpeed * Time.deltaTime * 10f);
        }
        else
        {
            transform.position += Vector3.up * SinkSpeed;
        }
    }

    void Dead()
    {
        
        drowgravity += SinkSpeed;
        pseudorotation = Quaternion.Lerp(pseudorotation, Quaternion.LookRotation(Vector3.down, Vector3.up), Time.deltaTime);
        transform.rotation = pseudorotation;
        transform.Rotate(Vector3.right * 90);
        transform.Rotate(Vector3.up * 180);

        transform.position += Vector3.down * drowgravity;
    }
}
