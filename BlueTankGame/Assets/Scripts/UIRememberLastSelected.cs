using UnityEngine;
using UnityEngine.EventSystems;

public class RememberCurrentSelectedObject : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject lastSelectedElement;


    void Reset()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();

        if (!eventSystem)
        {
            Debug.Log("Did not find event system in this scene");
            return;
        }

        lastSelectedElement = eventSystem.firstSelectedGameObject;
    }

    void Update()
    {
        if (!eventSystem) { return; }

        if (eventSystem.currentSelectedGameObject && lastSelectedElement != eventSystem.currentSelectedGameObject)
        {
            lastSelectedElement = eventSystem.currentSelectedGameObject;
        }

        if (!eventSystem.currentSelectedGameObject && lastSelectedElement)
        {
            eventSystem.SetSelectedGameObject(lastSelectedElement);
        }
    }
}
