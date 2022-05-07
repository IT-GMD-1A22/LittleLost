using UnityEngine;

namespace AS
{
    public class EndLevelScript : MonoBehaviour
    {
        [SerializeField] private string levelToEnter = "TelevisionScene";
    
        private void OnTriggerEnter(Collider other)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelToEnter);
        }
    }
}
