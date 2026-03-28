using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void CloseView(GameObject view)
    {
        view.SetActive(false);
    }
}