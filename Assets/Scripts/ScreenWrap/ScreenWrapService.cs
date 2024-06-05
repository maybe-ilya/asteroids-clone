using JetBrains.Annotations;
using MIG.API;

namespace MIG.ScreenWrapping
{
    [UsedImplicitly]
    public sealed class ScreenWrapService : IScreenWrapService
    {
        private readonly IScreenWrapDataProvider _dataProvider;
        private readonly IScreenWrapper _screenWrapper;

        public ScreenWrapService(IScreenWrapper screenWrapper, IScreenWrapDataProvider dataProvider)
        {
            _screenWrapper = screenWrapper;
            _dataProvider = dataProvider;
        }

        public void Init()
        {
            var (height, width) = _dataProvider.GetScreenDimensions();
            _screenWrapper.Setup(height, width);
        }
    }
}