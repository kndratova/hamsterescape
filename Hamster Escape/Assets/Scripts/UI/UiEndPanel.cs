using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UiEndPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText, maxScoreText;
    
        private void OnEnable()
        {
            scoreText.text = Score.CurrentScore.ToString();
            maxScoreText.text = Score.MaxScore.ToString();
        }
    }
}
