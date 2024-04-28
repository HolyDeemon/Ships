using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public KeyCode Fire = KeyCode.Space;
    public KeyCode Escape = KeyCode.Escape;

    public GameObject BallPrefab;

    public Transform cannon;
    public Transform LeftWheel;
    public Transform RightWheel;
    public Transform Muzzle;
    public ParticleSystem FireParticles;

    public float RotateSpeed;
    public float UpSpeed;
    public float energy;
    public static float ReloadTime = 1f;

    public static float reload = 0f;

    private Vector3 StartPos;
    private Vector2 Vel;

    public static float Horizontal;
    public static float Vertical;

    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        reload = Mathf.Clamp(reload - Time.deltaTime, 0, reload);

        Horizontal = Mathf.Clamp(Horizontal + Vel.x, -45, 45);
        Vertical = Mathf.Clamp(Vertical - Vel.y, -45, 10);

        transform.rotation = Quaternion.Euler(0, Horizontal , 0) ;
        cannon.rotation = Quaternion.Euler(0, Horizontal + 90, Vertical);

        Vel = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        LeftWheel.Rotate(Vector3.back * Vel.x * 2);
        RightWheel.Rotate(Vector3.forward * Vel.x * 2);

        if (Input.GetKey(Fire) && reload == 0)
        {
            reload += ReloadTime;

            var ball = Instantiate(BallPrefab, Muzzle.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, -90, -Vertical) * cannon.forward * energy);

            FireParticles.Play();
            FireParticles.gameObject.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKey(Escape))
        {
            Application.Quit();
        }
    }
}
