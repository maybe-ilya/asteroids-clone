using JetBrains.Annotations;
using MIG.API;

namespace MIG.Scores
{
    [UsedImplicitly]
    public sealed class JsonLeaderboardHandlerFactory : ILeaderboardHandlerFactory
    {
        private readonly IFileService _fileService;
        private readonly JsonLeaderboardHandlerSettings _handlerSettings;

        public JsonLeaderboardHandlerFactory(
            IFileService fileService,
            JsonLeaderboardHandlerSettings handlerSettings
        )
        {
            _handlerSettings = handlerSettings;
            _fileService = fileService;
        }

        public ILeaderboardHandler Create()
            => new JsonLeaderboardHandler(_fileService, _handlerSettings);
    }
}