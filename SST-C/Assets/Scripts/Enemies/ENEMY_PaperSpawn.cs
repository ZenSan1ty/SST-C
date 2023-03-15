using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ENEMY_PaperSpawn : MonoBehaviour
{
    private ENEMY_Paper mainScript;
    //[SerializeField] private GameObject paperEnemy;
    private Vector3 position;
    private float moveSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        mainScript = GetComponent<ENEMY_Paper>();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if (position.y >= -4)
        {
            mainScript.enabled = true;
            Destroy(this);
        }
        position.y += moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}
