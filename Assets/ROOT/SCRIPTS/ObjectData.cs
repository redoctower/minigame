using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectData : MonoBehaviour, IDropHandler
{
    public List<Sprite> subobjects;
    [SerializeField] private Sprite icon;
    public List<GameObject> activesubs;
    public ProgressManager _progressManager;
    public int health;

    private void Awake()
    {
        var img = GetComponent<Image>();
        img.sprite = icon;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            ProgressManager.curAttempt--;
            if (activesubs.Contains(eventData.pointerDrag.gameObject)) 
            {
                eventData.pointerDrag.gameObject.GetComponent<DragItem>().OnDrop(eventData);
                CorrectDrag(eventData.pointerDrag.gameObject);
            }
            _progressManager.CheckProgress();
        }
    }

    public void CorrectDrag(GameObject obj)
    {
        health--;
        obj.SetActive(false);

        if (health > 0) { }
        else ProgressManager.correctObj++;
    }
}
