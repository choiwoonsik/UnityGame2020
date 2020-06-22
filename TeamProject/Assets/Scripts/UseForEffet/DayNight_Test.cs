using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight_Test : MonoBehaviour
{
    public Animator animator;
    
    public DateManager thedate;


    // Start is called before the first frame update
    void Start()
    {
        thedate = FindObjectOfType<DateManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (DateManager.activated)
        {
            animator.speed = 4/thedate.updateTime;
            if (thedate.hour >= 6 && thedate.hour < 18)
            {
                animator.SetBool("Day", true);
            }
            else
                animator.SetBool("Day", false);
        }
        else
            animator.speed = 0;
    }
}
