public class ImmunityGranted : GameEvent
{
    public bool IsImmune = true;

    public ImmunityGranted (bool isImmune)
    {
        IsImmune = isImmune;
    }
}
