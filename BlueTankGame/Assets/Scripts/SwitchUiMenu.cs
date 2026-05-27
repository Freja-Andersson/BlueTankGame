using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchMenuToStillSelect : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable elementToSelect;

    [Header("Visual")]
    [SerializeField] bool showVisuals;
    [SerializeField] Color navigationColor = Color.cyan;

    void OnDrawGizmos()
    {
        if (!showVisuals) { return; }

        if (elementToSelect == null) { return; }

        Gizmos.color = navigationColor;
        Gizmos.DrawLine(gameObject.transform.position, elementToSelect.gameObject.transform.position);
    }

    void Reset()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
        if (eventSystem == null) { Debug.Log("Did not find event system in your scene"); }
    }

    public void JumpToElement()
    {
        if (eventSystem == null) { Debug.Log("has no event system referenced"); }

        if (elementToSelect == null) { Debug.Log("This should jump where?"); }

        eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }

}
