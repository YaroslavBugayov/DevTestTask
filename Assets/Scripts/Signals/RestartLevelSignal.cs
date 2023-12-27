using UnityEngine.SceneManagement;

namespace Signals
{
    public class RestartLevelSignal
    {
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}