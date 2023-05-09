using System;
using UnityEngine.UI;

public class AddTradeRouteDialog : UIView
{
    public Toggle toggleTask;

    public Text taskCost;

    public Task[] tasks;

    private Action<Task> action;

    private Task currTask;

    public void Show(Action<Task> action)
    {
        this.action = action;
        this.gameObject.SetActive(true);

        this.toggleTask.gameObject.SetActive(false);

        foreach (var task in tasks)
        {
            var newToggle = Instantiate(toggleTask, toggleTask.transform.parent);
            newToggle.gameObject.SetActive(true);

            newToggle.onValueChanged.AddListener(flag =>
            {
                if(flag)
                {
                    ShowTask(task);
                }
            });
        }
    }

    private void ShowTask(Task task)
    {
        currTask = task;

        taskCost.text = currTask.cost.ToString();
    }

    public void OnConfirm()
    {
        action?.Invoke(currTask);
        Destroy(this.gameObject);
    }

    public void OnCancel()
    {
        Destroy(this.gameObject);
    }
}
