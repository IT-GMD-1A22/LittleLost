using UnityEngine;

/*
 * Used to snap player to plane.
 * Can also unsnap when player arrive at destination
 *
 * JH
 */
public class SnapToObject : MonoBehaviour
{
    private GameObject player;
    public bool shouldSnapPlayer;

    private void Update()
    {
        if (player != null)
        {
            var parentTransform = transform.parent.transform;

            player.transform.position = parentTransform.position;
            player.transform.rotation = parentTransform.rotation * new Quaternion(1f, 90f, 1f, 1f);
        }
    }

    private void snapPlayer(Collider other)
    {
        player = other.gameObject;
        player.GetComponent<Animator>().SetBool("NoAnimation", true);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerLaneChanger>().enabled = false;
    }

    public void unSnapPlayer()
    {
        if (player)
        {
            player.GetComponent<Animator>().SetBool("NoAnimation", false);
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerLaneChanger>().enabled = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (shouldSnapPlayer)
            {
                snapPlayer(other);
            }
            
        }
    }
}