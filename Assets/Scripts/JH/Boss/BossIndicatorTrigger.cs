using UnityEngine;

/*
 * Tells boss manager indicator has been activated.
 * JH
 */
public class BossIndicatorTrigger : MonoBehaviour
{
    private BossLevelManager _bossLevelManager;

    private void Awake()
    {
        _bossLevelManager = FindObjectOfType<BossLevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _bossLevelManager.IndicatorTriggered();
        }
    }
}
