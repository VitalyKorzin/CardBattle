public class GreetingTextDisplay : TextDisplay
{
    protected override string GetEnglishText() 
        => "Welcome to the game - Card Battle!";

    protected override string GetRussianText() 
        => "Добро пожаловать в игру - Карточная битва!";

    protected override string GetTurkishText()
        => "Oyuna hoş geldiniz - Kart Savaşı!";
}