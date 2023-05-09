using System;

public class Facade
{
    public Clan playerClan { get; }

    public Facade()
    {
        playerClan = new Clan();
    }

    public void Run()
    {
        playerClan.cashManager.Update();
        playerClan.UpdateTask();
        //playerClan.taskManager.Update();
    }
}