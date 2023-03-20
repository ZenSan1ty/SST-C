using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBossSpinnyEnemy : MonoBehaviour
{
    public float lifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
