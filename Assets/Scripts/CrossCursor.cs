using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class CrossCursor : MonoBehaviour
{
    public GameObject circle;
    private LineRenderer cRenderer;


    private CircleCollider2D col;


    [Header("Selection Circle Settings")]
    [SerializeField] private float maxRadius = 1f;
    [SerializeField] private float minRadius = 0.1f;


    [Header("Grow Speed Circle Settings")]
    [SerializeField] private float speedUp = 1.2f;
    [SerializeField] private float speedDown = 0.8f;


    [Header("Lag Buffer before circle reducing")]
    [SerializeField] private float lagBuffer = 0.2f;
    private float lagBufferCounter;


    [Header("Mouse Speed Threshold")]
    [Range(0f, 5f)] [SerializeField] private float speedThreshold = 0.1f;


    [Space]
    [Space]
    [Header("UI SETTINGS")]
    [SerializeField] private TextMeshProUGUI MinRadiusText;
    [SerializeField] private TextMeshProUGUI MaxRadiusText;
    [SerializeField] private TextMeshProUGUI SpeedUpText;
    [SerializeField] private TextMeshProUGUI SpeedDownText;
    [SerializeField] private TextMeshProUGUI LagBufferText;
    [SerializeField] private TextMeshProUGUI SpeedThresholdText;


    List<GameObject> itemList = new List<GameObject>();


    private float currentRadius;

    private bool checkClick = false;
    bool hasStarted = false;

    private Vector2 currentMousePosition;
    private Vector2 lastMousePosition;
    private Vector2 delta;


    [Header("Events")]
    [Space]

    public UnityEvent OnMissedClick;

    void Awake()
    {
        if (OnMissedClick == null)
            OnMissedClick = new UnityEvent();
    }


    void Start()
    {
        cRenderer = circle.GetComponent<LineRenderer>();
        DrawCircle(1000, 0);
        gameObject.SetActive(false);
    }

    public void StartExpCursor()
    {
        Cursor.visible = false;
        currentMousePosition = new Vector2(transform.position.x, transform.position.y);

        currentRadius = minRadius;
        col = GetComponent<CircleCollider2D>();
        col.radius = minRadius;

        circle.SetActive(false);
        hasStarted = true;
        //col.enabled = false;
    }


    void Update()
    {
        if (hasStarted)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;

            if (Input.GetMouseButtonDown(0))
            {
                checkClick = true;
            }

            ListWatcher();

            lastMousePosition = currentMousePosition;
            currentMousePosition = new Vector2(transform.position.x, transform.position.y);

            delta = currentMousePosition - lastMousePosition;
            delta.x = delta.x / Screen.width;
            delta.y = delta.y / Screen.height;

            float speed = delta.magnitude * 1000;

            if (currentMousePosition != lastMousePosition)
            {
                lagBufferCounter = Time.time + lagBuffer;

                if ((currentRadius * speedUp) < maxRadius && speed >= speedThreshold)
                {
                    currentRadius *= speedUp;
                    DrawCircle(1000, currentRadius);
                    col.radius = currentRadius;
                }

            }
            else if (currentRadius > minRadius && lagBufferCounter <= Time.time && speed == 0)
            {
                currentRadius *= speedDown;
                DrawCircle(1000, currentRadius);
                col.radius = currentRadius;
            }


            if (currentRadius > minRadius)
            {
                circle.SetActive(true);
                //col.enabled = true; ;
            }
            else
            {
                circle.SetActive(false);
                //col.enabled = false;
            }
        }
    }

    public void DrawCircle(int steps, float radius)
    {
        cRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            cRenderer.SetPosition(currentStep, currentPosition);
        }
    }


    public void AddToList(GameObject obj)
    {
        itemList.Add(obj);
    }

    public void RemoveFromList(GameObject obj)
    {
        itemList.Remove(obj);
    }

    private ItemCircle CheckNearestObj()
    {

        //GameObject minObj = itemList.Min(x => Vector3.Distance(x.transform.position, transform.position));
        //minObj.GetComponent<ItemCircle>().SetHovering(true);

        int index = 0;
        int minOBJindex = 0;
        float distance = 0;

        foreach (GameObject obj in itemList)
        {
            if (index == 0)
            {
                minOBJindex = index;
                distance = Vector3.Distance(obj.transform.position, transform.position);
            }
            else
            {
                if (Vector3.Distance(obj.transform.position, transform.position) < distance)
                {
                    minOBJindex = index;
                    distance = Vector3.Distance(obj.transform.position, transform.position);
                }
            }

            itemList[index].GetComponent<ItemCircle>().SetHovering(false);
            index++;
        }

        itemList[minOBJindex].GetComponent<ItemCircle>().SetHovering(true);

        return itemList[minOBJindex].GetComponent<ItemCircle>();
    }


    private void ListWatcher()
    {
        ItemCircle circleObj = null;

        if (itemList.Count == 0)
        {
            if (checkClick)
                OnMissedClick.Invoke();

            checkClick = false;
            return;
        }
        else if (itemList.Count == 1)
        {
            circleObj = itemList[0].GetComponent<ItemCircle>();
            circleObj.SetHovering(true);

            if (checkClick && !(circleObj.isTheOne()))
                OnMissedClick.Invoke();

            checkClick = false;
        } 
        else
        {
            circleObj = CheckNearestObj();
            
            if (checkClick && !(circleObj.isTheOne()))
                OnMissedClick.Invoke();

            checkClick = false;
        }
    }

    public void displayEndParameters()
    {
        MinRadiusText.SetText("Rayon minimal : \t\t\t\t" + minRadius);
        MaxRadiusText.SetText("Rayon maximal : \t\t\t\t" + maxRadius);
        SpeedUpText.SetText("Vitesse d'agrandissement : \t\t" + speedUp);
        SpeedDownText.SetText("Vitesse de rétrécissement : \t\t" + speedDown);
        LagBufferText.SetText("Temps avant rétrécissement : \t\t" + lagBuffer);
        SpeedThresholdText.SetText("Seuil de vitesse : \t\t\t\t" + speedThreshold);
    }

    public void SetMinRadius(float value)
    {
        minRadius = value;
    }

    public void SetMaxRadius(float value)
    {
        maxRadius = value;
    }

    public void SetSpeedUp(float value)
    {
        speedUp = value;
    }

    public void SetSpeedDown(float value)
    {
        speedDown = value;
    }

    public void SetLagBuffer(float value)
    {
        lagBuffer = value;
    }

    public void SetSpeedT(float value)
    {
        speedThreshold = value;
    }

    public void SetStarted(bool value)
    {
        hasStarted = value;
    }
}
