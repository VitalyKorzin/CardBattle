public class ShopOpeningTextDisplay : TextDisplay
{
    protected override string GetEnglishText() 
        => "You don't have any cards right now. They can be bought at the store. Go to the store.";

    protected override string GetRussianText() 
        => "У вас сейчас нету карт. Их можно купить в магазине. Зайдите в магазин.";

    protected override string GetTurkishText() 
        => "Şu anda hiç kartınız yok. Mağazadan satın alınabilirler. Mağazaya Git.";
}