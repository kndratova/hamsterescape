using System;
using Player;
using UnityEngine;

namespace Core
{
    public class Score : MonoBehaviour
    {
        [Tooltip("Multiplier applied to the score.")]
        public float scoreMultiplier = 1;
        public static int CurrentScore { get; private set; }
        public static int MaxScore { get; private set; }
        
        [Tooltip("Rounding power used for rounding the score.")]
        [SerializeField] private uint roundingPower = 2;
    
        private int _roundingFactor;

        private void Start()
        {
            MaxScore = DataLoader.LoadMaxScore();
            _roundingFactor = (int)Math.Pow(10, roundingPower);
            CountScore();
        }

        private void Update()
        {
            CurrentScore = CountScore();
        }
    
        private int CountScore()
        {
            var rawScore = (int)DistanceCounter.Distance * scoreMultiplier;
            var roundedScore = RoundToNearest(rawScore, _roundingFactor);
            CurrentScore = roundedScore;
            return CurrentScore;
        }

        public static void CheckMaxScore()
        {
            if (CurrentScore > MaxScore)
            {
                MaxScore = CurrentScore;
                DataLoader.SaveMaxScore(MaxScore);
            }
        }
    
        private static int RoundToNearest(float value, int factor)
        {
            return (int)Math.Round((double)value / factor) * factor;
        }
    }
}