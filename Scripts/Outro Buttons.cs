using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroButtons : MonoBehaviour
{
    public GameObject good_ending_screen;
    public GameObject bad_ending_screen;
    public GameObject witch_screen;
    public GameObject witch_good;
    public GameObject witch_bad;
    // Start is called before the first frame update
    void Start()
    {
        if (StaticValues.killCount == 0)
        {
            good_ending_screen.SetActive(true);
        }
        else
        {
            bad_ending_screen.SetActive(true);
        }

        StartCoroutine(ShowWitchScreen());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShowWitchScreen()
    {

        yield return new WaitForSeconds(3f);
        good_ending_screen.SetActive(false);
        bad_ending_screen.SetActive(false);
        witch_screen.SetActive(true);
        if (StaticValues.killCount == 0)
        {
            witch_good.SetActive(true);
            witch_bad.SetActive(false);
        }
        else
        {
            witch_good.SetActive(false);
            witch_bad.SetActive(true);
        }

    }
}
