using DynamicData.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : UIItemView
{
    public Text costDay;
    public Text reserveDay;

    public Task task => (Task)model;

    // Start is called before the first frame update
    void Start()
    {
        SubScribe(task.WhenValueChanged(x => x.costDay), day => costDay.text = day.ToString());
        SubScribe(task.WhenValueChanged(x => x.reserveDay), day => reserveDay.text = day.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
