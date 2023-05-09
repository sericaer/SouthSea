using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class MainView : UIView
{
    public Text clanCache;

    public TaskItem defaultTaskItem;
    public AddTradeRouteDialog addTradeRouteDialog;

    private Facade facade;

    public void AddTradRoute()
    {
        var dialog = Instantiate(addTradeRouteDialog, addTradeRouteDialog.transform.parent);
        dialog.tasks = Enumerable.Range(0, 3).Select(_ => facade.playerClan.CreateTradeRouteTask()).ToArray();

        dialog.Show(task => facade.playerClan.AddTask(task));
    }

    // Start is called before the first frame update
    void Start()
    {
        facade = new Facade();

        SubScribe(facade.playerClan.cashManager.WhenValueChanged(x => x.current), cash=>clanCache.text = cash.ToString());

        SubScribe(facade.playerClan.tasks, defaultTaskItem);
    }

    int count = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if((count++)%100 == 0)
        {
            facade.Run();
        }
    }
}


public class UIView : MonoBehaviour
{
    private CompositeDisposable disposables;

    protected void SubScribe<T>(IObservable<T> observable, Action<T> action)
    {
        if (disposables == null)
        {
            disposables = new CompositeDisposable();
        }

        disposables.Add(observable.Subscribe(action));
    }

    protected void SubScribe<T>(IObservableList<T> observableList, UIItemView itemView)
    {
        itemView.gameObject.SetActive(false);

        if (disposables == null)
        {
            disposables = new CompositeDisposable();
        }

        var disposeAdd = observableList.Connect().OnItemAdded(item =>
        {
            var newView = itemView.Clone();
            newView.model = item;
        }).Subscribe();

        var disposeRemove = observableList.Connect().OnItemRemoved(item =>
        {
            var oldView = itemView.transform.parent.GetComponentsInChildren<UIItemView>().SingleOrDefault(x => x.model == (object)item);
            if(oldView!= null)
            {
                Destroy(oldView.gameObject);
            }
        }).Subscribe();

        disposables.Add(disposeAdd);
        disposables.Add(disposeRemove);
    }

    protected virtual void OnDestroy()
    {
        disposables?.Dispose();
        disposables = null;
    }
}