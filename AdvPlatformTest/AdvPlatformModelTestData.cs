namespace AdvPlatformTest
{
    public class AdvPlatformModelTestData
    {
        public static IEnumerable<object[]> ValidPlaceAndLocation()
        {
            yield return new object[] { "Яндекс.Директ", "/ru" };
            yield return new object[] { "Крутая реклама", "/ru/msk" };
        }
        public static IEnumerable<object[]> NullOrEmptyPlatformName()
        {
            yield return new string[] { "", "/ru" };
            yield return new string[] { "  ", "/ru/msk" };
            yield return new string[] { null, "/ru/permobl" };
        }
        public static IEnumerable<object[]> NullOrEmptyLocation()
        {
            yield return new object[] { "Газета уральских москвичей", " " };
            yield return new object[] { "Яндекс.Директ", "  " };
            yield return new object[] { "Крутая реклама", null };
        }
        public static IEnumerable<object[]> InvalidLocationFormat() 
        {
            yield return new object[] { "Газета уральских москвичей", "ru" }; // В локации нет начального "/"
            yield return new object[] { "Яндекс.Директ", "/ru/" }; //Локация заканчивается на "/"
            yield return new object[] { "Крутая реклама", "/ru/msk/123" }; // Цифры в локации
            yield return new object[] { "Крутая реклама", "/ru/msk/!" }; // Недопустимый символ в локации
            yield return new object[] { "Крутая реклама", "/ru/msk/smthtoolonglocation" }; // Слишком длинная часть в локации
            yield return new object[] { "Крутая реклама", "/ru/msk/s" }; // Слишком короткая часть в локации
            yield return new object[] { "Крутая реклама", "/ru/msk/with space" }; // Пробел в локации
        }
    }
}
