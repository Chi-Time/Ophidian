public class SpeedChanged : GameEvent
{
    public float Speed = 0.0f;

    public SpeedChanged (float speed)
    {
        Speed = speed;
    }
}