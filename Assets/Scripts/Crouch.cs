using UnityEngine;
using UnityEngine.TextCore.Text;

public class Crouch : MonoBehaviour 
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float crouchSpeed, normalHeight, crouchHeight;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;
    
    private bool crouching;

    void Update() {
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            crouching = !crouching;
        }

        if(crouching == true) {
            playerController.height = playerController.height - crouchSpeed * Time.deltaTime;
            if(playerController.height <= crouchHeight) {
                playerController.height = crouchHeight; 
            }
        }

        if(crouching == false) {

            if(playerController.height < normalHeight) {
                //player.gameObject.SetActive(false);
                player.position = player.position + offset * Time.deltaTime;
                //player.gameObject.SetActive(true);
            }

            playerController.height = playerController.height + crouchSpeed * Time.deltaTime;
            if(playerController.height >= normalHeight) {
                playerController.height = normalHeight; 
            }

        }

    }
}

