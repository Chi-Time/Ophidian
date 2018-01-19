public class SpeedChanged : GameEvent
{
    public float _Speed = 0.0f;

    public SpeedChanged (float speed)
    {
        _Speed = speed;
    }
}