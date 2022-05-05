using UnityEngine;

/*
 * Created with guidance and inspiration from JH's 'PlayerController'.
 *
 * Script to handle the character when climbing the wire
 * - NK
 */

public class Climbing : MonoBehaviour
{
    private PlayerController _controller;
    private CharacterController _characterController;
    private PlayerLaneChanger _playerLaneChanger;
    private PlayerAnimationController _playerAnimationController;
    [SerializeField] public bool _active = false;
    [SerializeField] private float ladderSpeed = 3.2f;
    private PlayerInputActionAsset _playerInput;
    
    private Transform ladder;

    
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInputActionAsset>();
        _controller = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
        _playerLaneChanger = GetComponent<PlayerLaneChanger>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            Debug.Log("Enter trigger");
            _active = true;
            ladder = other.transform;
            _playerInput.takeZMovement = true;
            _playerLaneChanger.enabled = false;
            _playerAnimationController.SetClimbAnimation(true);
        }

        if (other.CompareTag("LadderTop"))
        {
            Debug.Log("Ladder Top enter");
            _playerAnimationController.SetClimbAnimation(false);
            
            // Both methods are set again, in case OnTriggerExit is registered.
            _playerInput.takeZMovement = true;
            _playerLaneChanger.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder") || other.CompareTag("LadderTop"))
        {
            Debug.Log("Exit ladder");
            _active = false;
            _playerInput.takeZMovement = false;
            _playerLaneChanger.enabled = true;
            _playerAnimationController.SetClimbAnimation(false);
        }
    }
    
    void Update()
    {
        if (_active) {
            _playerLaneChanger.enabled = false;
            var destVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            var moveVector = Vector3.zero;
            if (_controller.GetInput().move.y > 0) {
                Vector3 diffVector = Vector3.MoveTowards(transform.position, destVector + new Vector3(0f, 0.5f, 0f), ladderSpeed * Time.deltaTime);
                moveVector = new Vector3(ladder.position.x, diffVector.y, ladder.position.z);
            }
            else if (_controller.GetInput().move.y < 0) {
                Vector3 diffVector = Vector3.MoveTowards(transform.position, destVector - new Vector3(0f, 0.5f, 0f), ladderSpeed * Time.deltaTime);
                moveVector = new Vector3(ladder.position.x, diffVector.y, ladder.position.z);
            }
        
            if (moveVector != Vector3.zero)
            {
                _characterController.enabled = false;
                _playerLaneChanger.enabled = false;
                transform.position = moveVector;
                _characterController.enabled = true;
                _playerLaneChanger.enabled = true;
            }
    
        }
    }
}
