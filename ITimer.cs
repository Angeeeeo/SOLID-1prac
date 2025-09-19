using System;

public interface ITimer
{
    void StartTimer(Action onEnd);
    void UpdateTimer(float timeLeft);
}