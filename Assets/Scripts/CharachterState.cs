public class CharachterState {

    public MovementType characheter_movement;
    public ActivityState charachter_activity;


    public CharachterState() {
        characheter_movement = MovementType.idle;
        charachter_activity = ActivityState.none;
    }

    public CharachterState(MovementType movement, ActivityState activity) {
        characheter_movement = movement;
        charachter_activity = activity;    
    }

}
