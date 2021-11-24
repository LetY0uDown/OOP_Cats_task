namespace OOP_Cats;
internal class CatSmartHouse
{
    public CatSmartHouse(int foodResource)
    {
        FoodResource = foodResource;
    }

    private List<Cat> cats = new List<Cat>();
    private static object printing = true;

    public int FoodResource { get; set; }
    public int CatsCount { get => cats.Count; }

    public void PrintStatus()
    {
        lock (printing)
        {
            var leftPosition = Console.CursorLeft;
            var topPosition = Console.CursorTop;

            for (int i = 0; i < CatsCount; i++)
            {
                var message = cats[i].GetInfo();
                var color = Convert.ToInt32(message.Substring(0, 2));

                Console.SetCursorPosition(0, i);
                Console.SetCursorPosition(0, i);

                Console.ForegroundColor = (ConsoleColor)color;
                Console.Write(message.Substring(2).PadLeft(50));
                Console.ResetColor();
            }

            Console.SetCursorPosition(0, cats.Count);
            Console.Write($"Еды осталось: {FoodResource}");
            Console.SetCursorPosition(leftPosition, topPosition);
        }
    }
    public void AddCats(Cat cat)
    {
        cats.Add(cat);
        //cat.HungryStatusChanged += Cat_HungryStatusChanged;
    }
    //public void Cat_HungryStatusChanged(object? sender, EventArgs e)
    //{
    //    Cat cat = (Cat)sender;
    //    cat.GetInfo();

    //    if (cat.HungryStatus <= 20 && FoodResource > 0)
    //    {
    //        sbyte needFood = (sbyte)(100 - cat.HungryStatus);
    //        if (FoodResource > needFood)
    //            FoodResource -= needFood;
    //        else
    //        {
    //            needFood = (sbyte)FoodResource;
    //            FoodResource = 0;
    //        }
    //        cat.Feed(needFood);
    //    }
    //}
}