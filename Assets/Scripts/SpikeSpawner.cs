using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpikeSpawner : MonoBehaviour
{
    public static SpikeSpawner instance;

    [SerializeField] private Spike _spike;
    private ObjectPool<Spike> _spikePool;

    [SerializeField] private BigSpike _bigSpike;
    private ObjectPool<BigSpike> _bigSpikePool;

    [SerializeField] private Ball _ball;
    private ObjectPool<Ball> _ballPool;

    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform[] ballSpawnPoints;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _spikePool = new ObjectPool<Spike>(() =>
        {
            return Instantiate(_spike);
        }, spike =>
        {
            spike.gameObject.SetActive(true);
        }, spike =>
        {
            spike.gameObject.SetActive(false);
        }, spike =>
        {
            Destroy(spike.gameObject);
        }, false, 100, 200);
        _bigSpikePool = new ObjectPool<BigSpike>(() =>
        {
            return Instantiate(_bigSpike);
        }, bigSpike =>
        {
            bigSpike.gameObject.SetActive(true);
        }, bigSpike =>
        {
            bigSpike.gameObject.SetActive(false);
        }, bigSpike =>
        {
            Destroy(bigSpike.gameObject);
        }, false, 100, 200);

        _ballPool = new ObjectPool<Ball>(() =>
        {
            return Instantiate(_ball);
        }, ball =>
        {
            ball.gameObject.SetActive(true);
        }, ball =>
        {
            ball.gameObject.SetActive(false);
        }, ball =>
        {
            Destroy(ball.gameObject);
        }, false, 100, 200);
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            if(GameManager.instance.score > 150)
            {
                SpawnBall();
            }
            else
                SpawnSpikes();


        }
    }
    public void SpawnSpikes()
    {
        if(timeBtwSpawns < 0)
        {
            int randomSpike = Random.Range(0, 2);
            Debug.Log(randomSpike);
            if(randomSpike == 0)
            {
                var smallSpike = _spikePool.Get();
                smallSpike.transform.position = spawnPoint.position;
                smallSpike.transform.rotation = Quaternion.identity;
            }
            if(randomSpike == 1)
            {
                var bigSpike = _bigSpikePool.Get();
                bigSpike.transform.position = spawnPoint.position;
                bigSpike.transform.rotation = Quaternion.identity;
            }
            
            timeBtwSpawns = startTimeBtwSpawns;
        }
        if(timeBtwSpawns > 0)
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
    public void SpawnBall()
    {
        if (timeBtwSpawns < 0)
        {
            var ball = _ballPool.Get();
            int randomBall = Random.Range(0, 3);
            if(randomBall == 0) 
                ball.transform.position = ballSpawnPoints[0].position;
            else if(randomBall == 1)
                ball.transform.position = ballSpawnPoints[1].position;
            else if(randomBall == 2)
                ball.transform.position = ballSpawnPoints[2].position;
            ball.transform.rotation = Quaternion.identity;

            timeBtwSpawns = startTimeBtwSpawns;
        }
        if (timeBtwSpawns > 0)
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
    public void ReleaseSmallSpike(Spike spike)
    {
        _spikePool.Release(spike);
    }
    public void ReleaseBigSpike(BigSpike spike)
    {
        _bigSpikePool.Release(spike);
    }
    public void ReleaseBall(Ball ball)
    {
        _ballPool.Release(ball);
    }
}
