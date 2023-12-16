namespace Chronometer;

internal interface IChronometer
{
    string GetTime { get; }

    List<string> Laps { get; }

    void Start();

    void Stop();

    string Lap();

    string LastLapTime { get; }

    void Reset();
}