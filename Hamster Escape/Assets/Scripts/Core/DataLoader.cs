using UnityEngine;

namespace Core
{
    public class DataLoader : MonoBehaviour
    {
        public static int LoadMaxScore() => PlayerPrefs.GetInt("MaxPlayerScore", 0);
    
        public static void SaveMaxScore(int value)
        {
            PlayerPrefs.SetInt("MaxPlayerScore", value);
            PlayerPrefs.Save();
        }
    }
}
