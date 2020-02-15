public class CharacterState {

    public MovementType character_movement;
    public ActivityState character_activity;


    public CharacterState() {
        character_movement = MovementType.idle;
        character_activity = ActivityState.none;
    }

    public CharacterState(MovementType movement, ActivityState activity) {
        character_movement = movement;
        character_activity = activity;    
    }

}
