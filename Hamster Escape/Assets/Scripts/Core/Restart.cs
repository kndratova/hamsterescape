using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Restart : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
