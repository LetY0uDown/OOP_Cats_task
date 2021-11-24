namespace OOP_Cats;
internal class Cat
{
    public Cat(string name, DateTime birthday)
    {
        Name = name;
        Birthday = birthday;

        Task.Run(LifeCircle);
    }

    private Random random = new Random();

    private sbyte _hungryStatus;
    private string[] states =
    {
            " умирает от голода",    // 0 DarkRed
            " очень голоден",        // 10 Magenta
            " очень голоден",        // 20 Magenta
            " очень голоден",        // 30 Magenta
            " хочет кушать",         // 40 DarkYellow
            " хочет кушать",         // 50 DarkYellow
            " хочет кушать",         // 60 DarkYellow
            " не против перекусить", // 70 Blue
            " не против перекусить", // 80 Blue
            " недавно поел",         // 90 Cyan
            " не голоден"            // 100 Green
    };
    public string Name { get; }
    public DateTime Birthday { get; }
    public sbyte HungryStatus
    {
        get
        {
            return _hungryStatus;
        }
        set
        {
            if (value < 0)
                value = 0;
            if (value > 100)
                value = 100;

            _hungryStatus = value;
            GetInfo();

            HungryStatusChanged?.Invoke(this, null);
        }
    }

    public event EventHandler HungryStatusChanged;

    public string GetInfo()
    {
        int color = 0;
        switch (HungryStatus / 10)
        {
            case 0:
                color = (int)ConsoleColor.DarkRed;
                break;
            case 1: case 2: case 3:
                color = (int)ConsoleColor.Magenta;
                break;
            case 4:  case 5: case 6:
                color = (int)ConsoleColor.DarkYellow;
                break;
            case 7: case 8:
                color = (int)ConsoleColor.Blue;
                break;
            case 9:
                color = (int)ConsoleColor.Cyan;
                break;
            case 10:
                color = (int)ConsoleColor.Green;
                break;
        }

        return color.ToString() + $"\nИмя: {Name}\nВозраст: {GetAge()}\n{Name + states[HungryStatus / 10]}\n";
    }
    public void Feed(sbyte food)
    {
        HungryStatus += food;
    }
    private int GetAge()
    {
        var today = DateTime.Now;
        var interval = today.Subtract(Birthday);
        var years = (int)interval.TotalDays / 365;
        return years;
    }
    private async Task LifeCircle()
    {
        await Task.Delay(random.Next(10, 50) * 100);
        GetInfo();
        HungryStatus -= (sbyte)random.Next(1, 11);
        await LifeCircle();
    }
}