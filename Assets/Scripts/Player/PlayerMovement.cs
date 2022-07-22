using UnityEngine;

/**
 * Übernommen von: https://www.youtube.com/watch?v=1P9S7lhX4KY
 */


    /*
     * Die Klasse ist für die Bewegung des Spielers durch den Benutzer verantwortlich
     */
public class PlayerMovement : MonoBehaviour
{

    /*
     * Vektor für die Richtung
     */ 
    private Vector3 moveDir = Vector3.zero;
    /*
     * Objekt des CharacterController
     */
    private CharacterController controller;
    /*
     * float Variable für die Gravitation
     */
    public float gravity = 25.0F;
    /*
     * float Variable für die Geschwindigkeit
     */ 
    public float walkspeed = 6.0F;
    /*
     * float Variable für die Sprunghöhe
     */ 
    public float jumpHeight = 8.0F;


    // Start Methode für die Controllerzeugung
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /*
     * Unity-Methode
     * 
     */

    void Update()
    {
        // Wird ausgeführt, solange Spieler sich auf dem Boden befindet
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= walkspeed;
            // Wird die Sprungtaste betätigt, erfolgt Zuordnung der y-Vektor Komponente
            if (Input.GetButton("Jump"))
                moveDir.y = jumpHeight;
        }
        moveDir.y -= gravity * Time.deltaTime;
        //Ausführung der Bewegung
        controller.Move(moveDir * Time.deltaTime);
    }


}
