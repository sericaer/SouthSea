using DynamicData;
using System;
using System.ComponentModel;
using System.Linq;

public class CashManager : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int current { get;  set; }

    public IObservableList<IIncome> incomes;

    public CashManager(IObservableList<IIncome> incomes)
    {
        this.incomes = incomes;

        current = incomes.Items.Sum(x => x.incomeValue) * 10;
    }

    public void Update()
    {
        current += incomes.Items.Sum(x => x.incomeValue);
    }
}