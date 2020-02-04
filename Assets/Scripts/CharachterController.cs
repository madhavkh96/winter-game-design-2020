using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterController : MonoBehaviour
{
    #region Variables
    [Header("Movement Customizations")]
    [Range(1f, 5f)]
    public int m_screenShake = 1;

    [Range(5f, 10f)]
    public int m_WalkMultiplier = 7;

    [Range(10f, 15f)]
    public int m_RunMultiplier = 12;

    [Range(1f, 7f)]
    public int m_DuckMultiplier = 4;

    [Range(1f, 15f)]
    public int m_XLookSensitivity = 10;

    [Range(1f, 15f)]
    public int m_YLookSensitivity = 10;

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
    bool duck_button;


    bool run_toggle;
    bool duck_toggle;

    Rigidbody rb;
    Transform parentTransform;
    [Header("Charachter States")]
    public CharachterState charachterState = new CharachterState();

    float rotationX;
    float rotationY;

    List<float> lookRotationInputs = new List<float>();
    List<float> movementInputs = new List<float>();

    Vector3 moveDirection;

    Vector3 duckHeight = new Vector3(1, 0.5f, 1);
    Vector3 normalHeight = new Vector3(1, 1, 1);

    #endregion

    private void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
        rb = GetComponentInParent<Rigidbody>();
        PopulateList(lookRotationInputs, 2);
        PopulateList(movementInputs, 2);
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
        duck_button = Input.GetButtonDown("Duck");

        // Detects the state the player currently is.
        charachterState = CurrentMovementStage();

        // Methods on the player
        Movement(vert_move, hor_move);

        RotateVertical(v_look);

        if (!StillLooking(lookRotationInputs))
            RotateHorizontal(h_look);

    }

    #region Movement And Rotation

    /// <summary>
    /// Moves the player in their local axes.
    /// </summary>
    /// <param name="vert"></param>
    /// <param name="hor"></param>
    void Movement(float vert, float hor) {


        Vector3 localMovementVec = Vector3.Normalize(transform.parent.localPosition);

        //Get the direction in the local co-ordinates of the player to move.
        moveDirection = transform.TransformDirection(new Vector3(localMovementVec.x * hor, 0, localMovementVec.z * vert));

        //Multiplier added to the given direction vector according to the Input State.

        if (charachterState.characheter_movement == MovementType.walk && charachterState.charachter_activity == ActivityState.none) 
        { 
            moveDirection *= m_WalkMultiplier;
            parentTransform.localScale = normalHeight;
        }
        else if (charachterState.characheter_movement == MovementType.run) { moveDirection *= m_RunMultiplier; }
        else if (charachterState.characheter_movement == MovementType.walk && charachterState.charachter_activity == ActivityState.duck) 
        {
            Debug.LogError("Here");
            moveDirection *= m_DuckMultiplier;
            parentTransform.localScale = duckHeight;
        }

        //Movement
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }


    /// <summary>
    /// Horizontal rotation done according to the given input float value; 
    /// 1.0f >= vert >= -1.0f
    /// </summary>
    /// <param name="hor"></param>
    void RotateHorizontal( float hor) {
        //Adds the rotation angle according to the input and sensitivity
        rotationX = hor * m_XLookSensitivity;

        // Makes the Vector according to along which axis the rotation should be added and by what magnitude.
        transform.localEulerAngles += new Vector3(0, rotationX, 0);
    }


    /// <summary>
    /// Vertical rotation done according to the given input float value; 
    /// 1.0f >= vert >= -1.0f
    /// </summary>
    /// <param name="vert"></param>
    void RotateVertical(float vert) {
        
        // Adds the rotation angle according to the input and sensitivity
        rotationY = vert * m_YLookSensitivity;
        
        // Makes the Vector according to along which axis the rotation should be and by what magnitude.
        Vector3 rotationYVector = new Vector3(rotationY, 0, 0);
        
        //Clamps the rotation between the specified values while rotating just the camera.
        if (Camera.main.transform.eulerAngles.x + rotationYVector.x > 45 &&
            Camera.main.transform.eulerAngles.x + rotationYVector.x < 300)
            return;
        else Camera.main.transform.eulerAngles += rotationYVector;

    }


    void Slide() { 
        
    }



    #endregion

    #region Helper Functions

    /// <summary>
    /// Checks if the player is still interacting with the Right Joystick in one movement.
    /// </summary>
    /// <param name="values"></param>
    /// <returns>bool</returns>
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

    /// <summary>
    /// Returns the current Movement State of the player according to the game state.
    /// </summary>
    /// <returns>CharahterState</returns>
    CharachterState CurrentMovementStage() {

        CharachterState currentCharachterState;

        /*
         * Adds the value of current frame and keeps the value of the previous frame to compare if the 
         * player is still moving around.             
         */
        movementInputs.Add(vert_move);

        if (movementInputs.Count >= 2)
        {
            movementInputs.RemoveAt(0);
        }


        /*
         * Adds the value of current frame and keeps the value of the previous frame to compare if the 
         * player is still moving their Right Joystick to look around.
         */
        lookRotationInputs.Add(h_look);

        if (lookRotationInputs.Count >= 2)
        {
            lookRotationInputs.RemoveAt(0);
        }


        //Checks if there is no input then return idle state.
        

        //Checks the value of the Run toggle to detect if player has pressed the Run Toggle for a state change.
        if (run_button)
        {
            run_toggle = true;
            duck_toggle = false;
        }

        if (duck_button) {
            duck_toggle = !duck_toggle;
        }

        if (duck_toggle) { run_toggle = false; }


        //MovementType and ActivityState Detection Logic.
        if (vert_move == 0 && hor_move == 0)
        {
            currentCharachterState = new CharachterState(MovementType.idle, ActivityState.none);
            Debug.Log("<color=red> Idle</color>");
            return currentCharachterState;
        }
        else if (run_toggle && Mathf.Abs(movementInputs[0]) > 0.85f && Mathf.Abs(movementInputs[1]) > 0.85f)
        {
            currentCharachterState = new CharachterState(MovementType.run, ActivityState.none);
            Debug.Log("<color=blue> Run, None</color>");
            return currentCharachterState;
        }
        else if (run_toggle && Mathf.Abs(movementInputs[0]) <= 0.85f && Mathf.Abs(movementInputs[1]) <= 0.85f)
        {
            run_toggle = false;
            Debug.Log("<color=green> Walk, None</color>");
            currentCharachterState = new CharachterState(MovementType.walk, ActivityState.none);
            return currentCharachterState;

        }
        else if (duck_toggle && charachterState.characheter_movement == MovementType.walk || charachterState.characheter_movement == MovementType.idle) {
            Debug.Log("<color=yellow> Walk, Duck</color>");
            currentCharachterState = new CharachterState(MovementType.walk, ActivityState.duck);
            return currentCharachterState;
        }
        else
        {
            Debug.Log("<color=green> Walk, None</color>");
            currentCharachterState = new CharachterState(MovementType.walk, ActivityState.none);
            return currentCharachterState;
        }

        

    }
    #endregion
}
