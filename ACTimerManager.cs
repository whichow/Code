using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTimerManager : MonoSingleton<ACTimerManager>
{
    public class ACTimer
    {
        public int timeId;
        public int runTimes;
        public float startTime;
        public float waitTime;
        public float timeInterval;
        public System.Action onTimeup;

        public void Start(System.Action onTimeup)
        {
            ACTimerManager.Instance.StartTimer(this, onTimeup);
        }

        public void Stop()
        {
            ACTimerManager.Instance.StopTimer(this);
        }
    }

    private int _timerIndex = 1;
    private List<ACTimer> _timer = new List<ACTimer>();
    private Queue<ACTimer> _unusedTimers = new Queue<ACTimer>();

    public ACTimer CreateTimer(float waitTime, float timeInterval)
    {
        ACTimer timer = _GetUnusedTimer();
        timer.timeId = _timerIndex;
        timer.runTimes = 0;
        timer.startTime = 0;
        timer.waitTime = waitTime;
        timer.timeInterval = timeInterval;

        _timerIndex++;
        return timer;
    }

    private ACTimer _GetUnusedTimer()
    {
        ACTimer timer;
        if (_unusedTimers.Count > 0)
        {
            timer = _unusedTimers.Dequeue();
        }
        else
        {
            timer = new ACTimer();
        }
        return timer;
    }

    public void StartTimer(ACTimer timer, System.Action onTimeup)
    {
        timer.startTime = Time.time;
        timer.onTimeup = onTimeup;
        _timer.Add(timer);
    }

    public void StopTimer(ACTimer timer)
    {
        _timer.Remove(timer);
        _unusedTimers.Enqueue(timer);
    }

    void Update()
    {
        for (int i = 0; i < _timer.Count; i++)
        {
            var timer = _timer[i];
            if (Time.time < timer.startTime + timer.waitTime + timer.timeInterval * timer.runTimes)
            {
                continue;
            }
            timer.runTimes++;
            if (timer != null)
            {
                timer.onTimeup();
            }
        }
    }
}
