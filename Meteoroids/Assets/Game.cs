using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player playerPrefab;
    public GameObject prePlayerClearer;
    public GameObject prePlayerExplosion;

    public Asteroids asteroidsPrefab;

    public FlyingSaucers flyingSaucersPrefab;

    public int score;

    public int livesRemaining = 2;

    private UI_Game ui;

    private void Start()
    {
        this.name = typeof(Game).Name;

        this.ui = GameObject.FindFirstObjectByType<UI_Game>();

        this.ui?.Score(this.score);

        this.ui?.Lives(this.livesRemaining);

        Instantiate(this.playerPrefab);

        Instantiate(this.asteroidsPrefab);

        Instantiate(this.flyingSaucersPrefab);
    }

    public void Score(int amount)
    {
        this.score = this.score + amount;

        this.ui?.Score(this.score);
    }

    public void PlayerWasDestroyed()
    {
        if (this.livesRemaining == 0)
        {
            this.gameEnded();
        }
        else
        {
            this.livesRemaining--;

            this.ui?.Lives(this.livesRemaining);

            Invoke("spawn1", 2.0f);
        }
    }

    private void spawn1()
    {
        Instantiate(this.prePlayerExplosion);

        Instantiate(this.prePlayerClearer);

        //Asteroid[] asteroids = GameObject.FindObjectsOfType<Asteroid>();

        //foreach (Asteroid a in asteroids)
        //{
            //a.GetComponent<Rigidbody>().AddExplosionForce(5.0f, new Vector3(0, 0, 0), 4.0f, 0, ForceMode.Impulse);
        //}

        Invoke("spawn2", 1.5f);
    }

    private void spawn2()
    {
        Instantiate(this.playerPrefab);
    }

    private void gameEnded()
    {
        Destroy(GameObject.FindFirstObjectByType<Asteroids>().gameObject);

        Destroy(GameObject.FindFirstObjectByType<FlyingSaucers>().gameObject);

        HighScores hs = GameObject.FindFirstObjectByType<HighScores>();

        if (hs.check(this.score))
        {
            Globals.ShowEnterNameScreen(this.score);
        }
        else
        {
            Globals.ShowHighScores();
        }

        Destroy(this.ui?.gameObject);

        Destroy(this.gameObject);
    }
}
