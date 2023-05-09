using System.ComponentModel;

public class TradeRoute : IIncome
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int incomeValue { get; set; }
}

public interface IIncome : INotifyPropertyChanged
{
    int incomeValue { get; }
}