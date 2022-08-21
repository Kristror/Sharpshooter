using UnityEngine;

public class GoalPanel : MonoBehaviour
{
    [SerializeField] private float timeOnScreen = 10;
    void Start()
    {
        Destroy(this, timeOnScreen);
    }
}
