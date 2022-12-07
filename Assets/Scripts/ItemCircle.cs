using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCircle : MonoBehaviour
{

    [SerializeField] private GameObject ccOBJ;
    private CrossCursor cc;

    [SerializeField] private GameObject hollowCircle;
    [SerializeField] private GameObject fullCircle;
    [SerializeField] bool TheOne = false;

    private bool isSelected = false;
    private bool isHovering = false;

    private Color onHover = new Color(0.2f, 0.8f, 0.7f, 0.5f);
    private Color outHover = new Color(1f, 1f, 1f, 0f);

    [Header("Events")]
    [Space]

    public UnityEvent OnCorrectClick;


    void Awake()
    {
        if (OnCorrectClick == null)
            OnCorrectClick = new UnityEvent();
    }

    
    void Start()
    {
        cc = ccOBJ.GetComponent<CrossCursor>();
    }


    void Update()
    {

        if (TheOne && !isSelected)
        {
            fullCircle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1f);
        }

        if (isHovering == true && isSelected == false && fullCircle.GetComponent<SpriteRenderer>().color != onHover)
        {
            fullCircle.GetComponent<SpriteRenderer>().color = onHover;
        }
        else if ((isHovering == false && isSelected == false && fullCircle.GetComponent<SpriteRenderer>().color != outHover && !TheOne))
        {
            fullCircle.GetComponent<SpriteRenderer>().color = outHover;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isHovering == false && isSelected == true)
            {
                isSelected = false;
                fullCircle.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }

            if (isHovering == true && isSelected == false)
            {
                isSelected = true;
                fullCircle.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1f);

                if (TheOne)
                {
                    isSelected = false;
                    OnCorrectClick.Invoke();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 3)
        {
            cc.AddToList(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 3)
        {
            cc.RemoveFromList(gameObject);
            isHovering = false;

            if (isSelected == false)
                fullCircle.GetComponent<SpriteRenderer>().color = outHover;
        }
    }


    public void SetHovering(bool val)
    {
        isHovering = val;
    }

    public void SetSelected(bool val)
    {
        isSelected = val;
    }

    public bool isTheOne()
    {
        return TheOne;
    }
}
