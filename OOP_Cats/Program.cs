using OOP_Cats;

static Cat CreateCat()
{
    Console.Write("Введите имя вашего кота >> ");
    var name = Console.ReadLine();
    Console.Write("Введите дату рождения вашего кота в формате ДД.ММ.ГГ >> ");
    var inputBirthday = Console.ReadLine();

    DateTime birthday;

    if (name == "")
        name = "Стандартный кот";

    if (inputBirthday == "")
        birthday = DateTime.Now;
    else
        birthday = DateTime.Parse(inputBirthday);

    return new Cat(name, birthday) { HungryStatus = 100 };
}

Cat cat = CreateCat();
Console.WriteLine(cat.GetInfo().Substring(2));

CatSmartHouse catHouse = new CatSmartHouse(500);
catHouse.AddCats(cat);
cat.HungryStatusChanged += Cat_HungryStatusChanged;

Console.SetCursorPosition(0, catHouse.CatsCount * 7);
Console.ReadLine();

static void Cat_HungryStatusChanged(object? sender, EventArgs e)
{
    Cat cat = (Cat)sender;
    cat.GetInfo();

    CatSmartHouse house = new CatSmartHouse(500);

    if (cat.HungryStatus <= 20 && house.FoodResource > 0)
    {
        sbyte needFood = (sbyte)(100 - cat.HungryStatus);
        if (house.FoodResource > needFood)
            house.FoodResource -= needFood;
        else
        {
            needFood = (sbyte)house.FoodResource;
            house.FoodResource = 0;
        }
        cat.Feed(needFood);
    }
}