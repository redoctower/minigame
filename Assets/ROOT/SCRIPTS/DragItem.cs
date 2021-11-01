using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    [SerializeField] private Vector2 _cachpos;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        Invoke(nameof(CachPos), 0.1f);
    }

    void CachPos()
    {
        _cachpos = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        StartCoroutine(LerpFunction(transform, _cachpos, 0.15f));
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public static IEnumerator LerpFunction(Transform start, Vector2 end, float duration)
    {
        float time = 0;
        Vector2 startValue = start.localPosition;

        while (time < duration)
        {
            start.localPosition = Vector2.Lerp(startValue, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        start.localPosition = end;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.localPosition = _cachpos;
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
