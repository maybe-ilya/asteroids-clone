namespace MIG.ScreenWrapping
{
    public interface IScreenWrapDataProvider
    {
        (float, float) GetScreenDimensions();
    }
}