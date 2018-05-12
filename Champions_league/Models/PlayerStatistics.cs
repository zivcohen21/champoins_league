using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Champions_league.Models
{
    public class PlayerStatistics
    {
        public int PlayerId { get; set; }
        public long TrainingTimestamp { get; set; }
        public int GoalCount { get; set; }
        public int BallTouches { get; set; }
        public double TotalGameTime { get; set; }
        public int TotalRetrieves { get; set; }
        public int TotalPasses { get; set; }
        public double MotionScore { get; set; }
        public double PossessionTempo { get; set; }
        public int AccuratePasses { get; set; }

        public PlayerStatistics(int playerId)
        {
            PlayerId = playerId;
            TrainingTimestamp = 0;
            GoalCount = 0;
            BallTouches = 0;
            TotalGameTime = 0;
            TotalRetrieves = 0;
            TotalPasses = 0;
            MotionScore = 0;
            PossessionTempo = 0;
            AccuratePasses = 0;
        }

        public PlayerStatistics(int playerId, long trainingTimestamp, int goalCount, int ballTouches, double totalGameTime, int totalRetrieves, int totalPasses, double motionScore, double possessionTempo, int accuratePasses)
        {
            PlayerId = playerId;
            TrainingTimestamp = trainingTimestamp;
            GoalCount = goalCount;
            BallTouches = ballTouches;
            TotalGameTime = totalGameTime;
            TotalRetrieves = totalRetrieves;
            TotalPasses = totalPasses;
            MotionScore = motionScore;
            PossessionTempo = possessionTempo;
            AccuratePasses = accuratePasses;
        }
    }
}