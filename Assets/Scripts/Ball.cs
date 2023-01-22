using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
            if (transform.position.x < -3.25f)
            {
                SpikeSpawner.instance.ReleaseBall(this);
            }
        }
    }
}
