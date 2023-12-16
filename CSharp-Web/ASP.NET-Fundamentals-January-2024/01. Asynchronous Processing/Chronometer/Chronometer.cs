namespace Chronometer;

using System.Diagnostics;

public class Chronometer : IChronometer
{
    private readonly Stopwatch stopWatch;
    private readonly List<string> laps;
    private string lastLapTime;

    public Chronometer()
    {
        this.stopWatch = new Stopwatch();
        this.laps = new List<string>();
    }

    public string GetTime
        => this.stopWatch.Elapsed.ToString(@"mm\:ss\.ffff");

    public List<string> Laps
        => this.laps;

    public void Start()
        => this.stopWatch.Start();

    public void Stop()
        => this.stopWatch.Stop();


    public string Lap()
    {
        string result = this.GetTime;
        this.laps.Add(result);
        this.lastLapTime = result;

        return result;
    }

    public string LastLapTime
        => this.lastLapTime;

    public void Reset()
    {
        this.stopWatch.Reset();
        this.laps.Clear();
    }
}