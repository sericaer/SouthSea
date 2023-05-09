using DynamicData;
using System;
using System.ComponentModel;
using System.Linq;

public class Clan
{
    public CashManager cashManager { get; }

    public SourceList<TradeRoute> tradeRoutes { get; }

    public SourceList<Task> tasks { get; }

    public void UpdateTask()
    {
        foreach(var task in tasks.Items)
        {
            task.Update();
        }

        tasks.RemoveMany(tasks.Items.Where(t => t.isFinished).ToArray());
    }

    public Clan()
    {
        tradeRoutes = new SourceList<TradeRoute>();
        tasks = new SourceList<Task>();

        cashManager = new CashManager(tradeRoutes.Connect().Transform(x=>(IIncome)x).AsObservableList());

        AddTradeRoute(1);
        AddTradeRoute(2);
    }

    public void AddTradeRoute(int income)
    {
        var tradeRoute = new TradeRoute() { incomeValue = income };
        tradeRoutes.Add(tradeRoute);
    }

    internal Task CreateTradeRouteTask()
    {
        var random = new Random();
        
        return new Task(random.Next(50,100), random.Next(5, 10), random.Next(30, 40), (income)=> tradeRoutes.Add(new TradeRoute() { incomeValue = income }));
    }

    public void AddTask(Task task)
    {
        cashManager.current -= task.cost;

        tasks.Add(task);
    }
}

public class Task : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int cost { get; }
    public int income { get; }

    public bool isFinished { get; private set; }
    public int costDay { get; }
    public int reserveDay { get; private set; }

    private Action<int> successAction;

    public Task(int cost, int income, int costDay, Action<int> successAction)
    {
        this.cost = cost;
        this.income = income;
        this.costDay = costDay;
        this.reserveDay = costDay;
        this.successAction = successAction;
    }

    public void Update()
    {
        if(reserveDay-- == 0)
        {
            isFinished = true;
            successAction.Invoke(income);
        }
    }
}