public class ScoreIncreased : GameEvent
{
    public int Value = 0;

    public ScoreIncreased (int value)
    {
        Value = value;
    }
}
