using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Image _image;
    private LineRenderer _lineRenderer;
    private Canvas _canvas;
    private bool _isDragStarted = false;

    public bool isLeftWire;

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredtWire;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos
            );

            
            Vector3 worldPos = _canvas.transform.TransformPoint(movePos);

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, worldPos);
        }
        else
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }


    }


    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLeftWire)
        {
            return;
        }
        _isDragStarted = true;
        CurrentDraggedWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        _isDragStarted = false;
        CurrentDraggedWire = null;
    }
}
