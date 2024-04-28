using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public static float Score;
    public GameObject ShipPrefab;
    public float SpawnTimer;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, timer);
        if(timer <= 0)
        {
            timer = Mathf.Clamp(SpawnTimer - Mathf.Sqrt(Time.time / 120), 0, SpawnTimer);
            SummonShip(new Vector3(42, 0, Random.Range(-20, 10)));
        }
    }

    void SummonShip(Vector3 pos)
    {
        var ship = Instantiate(ShipPrefab, pos, Quaternion.identity);
    }
}
