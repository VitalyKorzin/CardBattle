public class UsingPlayerAbilityTextDisplay : TextDisplay
{
    protected override string GetEnglishText() 
        => "During the battle, you can use your ability. " +
        "It works just like maps. Point your ability at enemies.";

    protected override string GetRussianText() 
        => "Во время сражения вы можете использовать свою способность. " +
        "Она работает также, как и карты. Наведите свою способность на врагов.";

    protected override string GetTurkishText() 
        => "Savaş sırasında yeteneğinizi kullanabilirsiniz. " +
        "Tıpkı haritalar gibi çalışır. Yeteneğinizi düşmanlara doğrultun.";
}