using System.Collections;
using UnityEngine;

/*
 * Knock back the player with force
 * Acts as a trigger, as such can also be handled by level trigger manager
 */
public class KnockBackTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private float waitForSeconds = 1f;
    [SerializeField] private float strength = 5f;
    [SerializeField] private Vector3 pushDirection;
    public bool isCompleted { get; set; }

    public void Invoke()
    {
        StartCoroutine(KnockBackPlayer());
    }

    private IEnumerator KnockBackPlayer()
    {
        var controller = SpawnManager.Instance.currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;
        controller.AddExternalForce(pushDirection, strength);
        yield return new WaitForSeconds(waitForSeconds);
        controller.disablePlayerInput = false;

        isCompleted = true;
    }
}