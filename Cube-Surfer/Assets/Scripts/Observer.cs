using UnityEngine.Events;
public class Observer
{
    public static UnityAction HIT;
    public static UnityAction FinalGame;

    public static UnityAction<GameState> UpdateGameState;

}
