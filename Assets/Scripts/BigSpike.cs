using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpike : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
            if (transform.position.x < -3.25f)
            {
                SpikeSpawner.instance.ReleaseBigSpike(this);
            }
        }
        
        //if (GameManager.instance.isGameOver)
        //{
        //    SpikeSpawner.instance.ReleaseBigSpike(this);
        //}
    }
}
