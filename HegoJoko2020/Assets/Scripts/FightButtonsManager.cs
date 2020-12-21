using UnityEngine;

public class FightButtonsManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonLayout;

    public void SetVisibility(bool b_visibility)
    {
        buttonLayout.SetActive(b_visibility);
    }
    public void ToggleVisibility()
    {
        SetVisibility(!buttonLayout.activeSelf);
    }
    protected void ClearContent()
    {
        foreach (Transform child in buttonLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
