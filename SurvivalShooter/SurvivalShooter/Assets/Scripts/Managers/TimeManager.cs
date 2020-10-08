using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public List<Timer> timers = new List<Timer>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Timer t in timers)
        {
            if(!t.finished)
            t.Update();
        }
    }
}

public delegate void TimerFunction();

public class Timer 
{
    public float duration = 1f;
    float elapsed = 0f;
    public bool finished = false;
    public TimerFunction onTimer;

    public Timer(float d, TimerFunction t)
    {
        duration = d;
        onTimer = t;
    }

    public void Update() 
    {
        elapsed += Time.deltaTime;
        Debug.Log(elapsed + onTimer.ToString());
        if(elapsed >= duration && !finished)
        {
            finished = true;
            Debug.Log("Finished: " + onTimer.ToString());
            if(onTimer != null)
                onTimer();
        }
    }

}

