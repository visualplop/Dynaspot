using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> Plateaux = new List<GameObject>();
    [SerializeField] private List<ItemCircle> AllCircles = new List<ItemCircle>();
    [SerializeField] private List<TextMeshProUGUI> TextUIList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI MissedClickText;
    [SerializeField] private TextMeshProUGUI MissedClickAVG;
    [SerializeField] private TextMeshProUGUI TimeTotal;
    [SerializeField] private TextMeshProUGUI TimeAVG;

    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject resultScreen;

    private int currentPlate = 0;
    private bool hasStarted = false;

    private int missedClick = 0;
    private List<float> timer = new List<float>();

    private float beginTime;
    private float totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(true);
        resultScreen.SetActive(false);
        cursor.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void StartExp()
    {
        cursor.GetComponent<CrossCursor>().StartExpCursor();
        menuScreen.SetActive(false);
        cursor.SetActive(true);
        Plateaux[currentPlate].SetActive(true);
        hasStarted = true;
        ComputeTiming(1);
    }

    private void Restart()
    {
        Cursor.visible = true;
        menuScreen.SetActive(true);
        resultScreen.SetActive(false);
        Plateaux[currentPlate].SetActive(false);
        currentPlate = 0;
        missedClick = 0;
        hasStarted = false;
        cursor.SetActive(false);
        totalTime = 0.0f;
        timer.Clear();
    }

    public void NextPlate()
    {
        ComputeTiming(2);

        if (currentPlate + 1 >= Plateaux.Count)
        {
            DisplayResults();
        }
        else
        {
            Plateaux[currentPlate].SetActive(false);
            currentPlate++;
            Plateaux[currentPlate].SetActive(true);
        }

        ComputeTiming(1);

        foreach (ItemCircle obj in AllCircles)
        {
            obj.SetSelected(false);
        }
    }

    public void MissedClick()
    {
        if (hasStarted)
            missedClick++;
    }

    public void ComputeTiming(int state)
    {
        if (state == 1)
        {
            beginTime = Time.time;
        } else if (state == 2)
        {
            timer.Add(Time.time - beginTime);
            totalTime += (Time.time - beginTime);
        }
    }

    private void DisplayResults()
    {
        float timeAVG = totalTime / Plateaux.Count;
        float missedAVG = (missedClick / (((float)Plateaux.Count) + missedClick)) * 100;

        cursor.GetComponent<CrossCursor>().displayEndParameters();
        cursor.GetComponent<CrossCursor>().SetStarted(false);

        Plateaux[currentPlate].SetActive(false);
        cursor.SetActive(false);

        int i = 0;
        foreach (TextMeshProUGUI txt in TextUIList)
        {
            txt.SetText("Plateau " + (i + 1) + " :\t" + timer[i] + "s");
            i++;
        }

        MissedClickText.SetText("Clics échoués : \t" + missedClick);
        MissedClickAVG.SetText("Taux d'échec : \t" + missedAVG + "%");
        TimeTotal.SetText("Temps total : \t" + totalTime + "s");
        TimeAVG.SetText("Temps moyen : \t" + timeAVG + "s");

        resultScreen.SetActive(true);
    }
}
