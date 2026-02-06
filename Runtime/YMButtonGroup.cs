using UnityEngine;

public class YMButtonGroup : MonoBehaviour
{
    public int InitialIndex = 0;
    private int currentIndex = -1;
    private void Awake()
    {
        currentIndex = InitialIndex;
        
    }
}
