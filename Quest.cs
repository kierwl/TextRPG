using System;

class Quest
{
    public string Title { get; }
    public string Description { get; }
    public int Goal { get; }
    public int Progress { get; set; }
    public int Reward { get; }
    public string Target { get; }

    public bool IsCompleted => Progress >= Goal;
    public void IncrementProgress()
    {
        if (Progress < Goal)
        {
            Progress++;
        }
    }
    public Quest(string title, string description, int goal, string target,int reward)// »ý¼ºÀÚ
    {
        Title = title;
        Description = description;
        Goal = goal;
        Target = target;
        Reward = reward;
    }
}
