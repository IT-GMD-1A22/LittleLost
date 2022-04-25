using UnityEngine;

/*
 * Snaps player to position
 */
public class Snapper : MonoBehaviour, ISnapper
{
    private GameObject player;

    public void Snap(Collider other)
    {
        player = other.gameObject;
        player.GetComponent<Animator>().SetBool("NoAnimation", true);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerLaneChanger>().enabled = false;
    }

    public void UnSnapPlayer()
    {
        if (player)
        {
            player.GetComponent<Animator>().SetBool("NoAnimation", false);
            player.GetComponent<PlayerLaneChanger>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    public void SetSnapPosition()
    {
        if (player != null)
        {
            var parentTransform = transform.parent.transform;

            player.transform.position = parentTransform.position;
            player.transform.rotation = parentTransform.rotation * new Quaternion(1f, 90f, 1f, 1f);
        }
    }
}