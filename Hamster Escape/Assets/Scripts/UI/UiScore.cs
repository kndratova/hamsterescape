using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UiScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;

        private void Update()
        {
            textField.text = Score.CurrentScore.ToString();
        }
    }
}
