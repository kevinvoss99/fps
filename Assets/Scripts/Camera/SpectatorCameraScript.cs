using UnityEngine;

/**
 * Dieses Skript stellt die Steuerung für die Zuschauerkamera zur Verfügung.
 */
public class SpectatorCameraScript : MonoBehaviour {

    /**
     * Geschwindigkeit der Kamera.
     */
    public float mSpeed = 12f;

    /**
     * Kameraobjekt.
     */
    private Camera mSpectatorCamera;

    /**
     * Bewegungsvektor.
     */
    private Vector3 mMovement;

    /**
     * Sichtweite.
     */
    private float mCamRayLength = 100f;

    /**
     * Variable zur Speicherung des Layer-Wertes des Bodens.
     */
    private int mFloorMask;

    /**
     * Geschwindigkeit auf der Y-Achse.
     */
    private float mSpeedYAxis = 6f;

    /**
     * Unity Methode, die beim Aufruf des Skripts *einmalig* ausgeführt wird.
     */
    private void Awake()
    {
        mSpectatorCamera = GetComponent<Camera>();
        mFloorMask = LayerMask.GetMask("Floor");
    }

    /**
     * Unity Methode
     */
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");



        if (Input.GetKey(KeyCode.Space))
        {
            Move(horizontal, vertical, true, false);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(horizontal, vertical, false, true);
        }
        else
        {
            Move(horizontal, vertical, false, false);
        }

        Turning();
        
    }

    /**
     * Methode, welche die Bewegung anhand der Tastatureingaben bewältigt.
     */
    private void Move(float horizontal, float vertical, bool up, bool down)
    {
        mMovement.Set(horizontal, up && !down ? mSpeedYAxis : !up && down ? -mSpeedYAxis : 0f, vertical);

        mMovement = mMovement.normalized * mSpeed * Time.deltaTime;


        mSpectatorCamera.transform.position = transform.position + mMovement;
    }

    /**
     * Methode, welche das Umsehen mit Hilfe der Maus ermöglicht.
     */
    private void Turning()
    {
        Ray camRay = mSpectatorCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, mCamRayLength, mFloorMask))
        {
            Vector3 cameraToMouse = floorHit.point - transform.position;
            cameraToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(cameraToMouse);

            mSpectatorCamera.transform.rotation = newRotation;
        }
    }
}
