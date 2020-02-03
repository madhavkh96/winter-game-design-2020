using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterController : MonoBehaviour
{

    [Header("Movement Customizations")]
    [Range(1f, 5f)]
    public int m_screenShake = 1;

    [Range(5f, 10f)]
    public int m_WalkMultiplier = 7;

    [Range(10f, 15f)]
    public int m_RunMultiplier = 12;


    [Range(1f, 360f)]
    public int m_lookSensitivity = 180;

    [Header("Horizontal-Look Restraints")]
    public float m_minimumXLook = -360f;
    public float m_maximumXLook =  360f;
 
    [Header("Vertical Look Restraints")]
    public float m_minimumYLook = -60f;
    public float m_maximumYLook =  60f;


    public int frameCounter = 10;

    //Inputs
    float vert_move;
    float hor_move;
    float v_look;
    float h_look;
    bool run_button;


    bool run_toggle;
   
    Rigidbody rb;
   
    public enum MovementType {
    idle,
    walk,
    run,
    }

    public enum ActivityState { 
    grapple,
    slide,
    wallrun,
    }

    public MovementType charachterMovement;

    float rotationX;
    float rotationY;

    public List<float> lookRotationInputs = new List<float>();
    public List<float> movementInputs = new List<float>();

    Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PopulateList(lookRotationInputs, 2);
        PopulateList(movementInputs, 2);
        charachterMovement = MovementType.idle;
    }

    void FixedUpdate()
    {
        Inputs();
    }

    
    void Inputs() {
        
        // Get Inputs from Joystick for Movement
        vert_move   = Input.GetAxis("Vertical");
        hor_move = Input.GetAxis("Horizontal");
        v_look = Input.GetAxis("Vertical Look");
        h_look = Input.GetAxis("Horizontal Look");
        run_button = Input.GetButtonDown("Run");

        charachterMovement = CurrentMovementStage();

        // Methods
        Movement(vert_move, hor_move);

        RotateVertical(v_look);

        if (!StillLooking(lookRotationInputs))
            RotateHorizontal(h_look, v_look);

    }
    #region Movement And Rotation
    void Movement(float vert, float hor) {

        Vector3 localMovementVec = Vector3.Normalize(transform.localPosition);

        moveDirection = transform.TransformDirection(new Vector3(localMovementVec.x * hor, 0, localMovementVec.z * vert));
        
        if (charachterMovement == MovementType.walk) { moveDirection *= m_WalkMultiplier; }
        else if (charachterMovement == MovementType.run) { moveDirection *= m_RunMultiplier; }


        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

    void RotateHorizontal( float hor, float vert) {
        rotationX = hor * m_lookSensitivity;
        transform.localEulerAngles += new Vector3(0, rotationX, 0);
    }

    void RotateVertical(float vert) {

        rotationY = vert * m_lookSensitivity;
        Vector3 rotationYVector = new Vector3(rotationY, 0, 0);
        

        if (Camera.main.transform.eulerAngles.x + rotationYVector.x > 45 &&
            Camera.main.transform.eulerAngles.x + rotationYVector.x < 300)
            return;
        else Camera.main.transform.eulerAngles += rotationYVector;

    }

    #endregion

    #region Helper Functions
    bool StillLooking(List<float> values) {

        if (Mathf.Abs(values[0]) > Mathf.Abs(values[1])) return true;
        else if (Mathf.Abs(values[0]) == 1 && Mathf.Abs(values[1]) == 1 
                || Mathf.Abs(values[0]) == -1 && Mathf.Abs(values[1]) == -1) return false;
        else if (Mathf.Abs(values[0]) == 0 && Mathf.Abs(values[1]) == 0) return true;
        else return false;
    }

    void PopulateList(List<float> values, int size) {
        for (int i = 0; i < size; ++i) {
            values.Add(0.0f);
        }
    }

    MovementType CurrentMovementStage() {

        if (run_button) run_toggle = true;

        movementInputs.Add(vert_move);

        if (movementInputs.Count >= 2)
        {
            movementInputs.RemoveAt(0);
        }

        lookRotationInputs.Add(h_look);

        if (lookRotationInputs.Count >= 2)
        {
            lookRotationInputs.RemoveAt(0);
        }


        Debug.Log(run_toggle);

        if (run_toggle && Mathf.Abs(movementInputs[0]) > 0.85f && Mathf.Abs(movementInputs[1]) > 0.85f)
            return MovementType.run;
        else if (run_toggle && Mathf.Abs(movementInputs[0]) <= 0.85f && Mathf.Abs(movementInputs[1]) <= 0.85f)
        {
            run_toggle = false;
            return MovementType.walk;
        }
        else return MovementType.walk;
    }
    #endregion
}
