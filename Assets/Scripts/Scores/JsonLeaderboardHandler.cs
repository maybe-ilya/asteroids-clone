using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using MIG.API;
using Newtonsoft.Json;
using UnityEngine;

namespace MIG.Scores
{
    [UsedImplicitly]
    public sealed class JsonLeaderboardHandler : ILeaderboardHandler
    {
        private readonly IFileService _fileService;
        private readonly JsonLeaderboardHandlerSettings _settings;

        private string JsonPath => Path.Combine(Application.persistentDataPath, _settings.FilePath);

        public JsonLeaderboardHandler(
            IFileService fileService,
            JsonLeaderboardHandlerSettings settings
        )
        {
            _fileService = fileService;
            _settings = settings;
        }

        public List<LeaderboardEntry> ReadHighScores()
        {
            if (!_fileService.DoesFileExist(JsonPath))
            {
                return new List<LeaderboardEntry>();
            }

            var json = _fileService.ReadText(JsonPath);
            return JsonConvert.DeserializeObject<List<LeaderboardEntry>>(json);
        }

        public void SaveHighScores(List<LeaderboardEntry> highScores)
        {
            var json = JsonConvert.SerializeObject(highScores);
            _fileService.WriteText(JsonPath, json);
        }
    }
}