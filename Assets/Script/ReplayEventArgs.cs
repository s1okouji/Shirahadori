public class ReplayEventArgs
{
    public bool isOut { get; set; }
    public ReplayEventArgs(bool isOut)
    {
        this.isOut = isOut;
    }
}