using xsolla_backend_card.Models;

namespace xsolla_backend_card.Logics
{
    public class Luhn
    {
        /// <summary>
        /// Упрощенный алгоритм Луна
        /// </summary>
        /// <param name="cardInfo">Сведения о банковской карте</param>
        /// <returns>true - номер карты валиден, false - иначе</returns>
        public bool SimplifiedAlg(CardInfo cardInfo)
        {
            int sum = 0;
            int len = cardInfo.Number.Length;
            int controlNum = len % 2;
            for (int i = 0; i < len; i++)
            {
                int addition = cardInfo.Number[i] - '0';
                if (i % 2 == controlNum)
                {
                    addition *= 2;
                    addition -= addition > 9 ? 9 : 0;
                }
                sum += addition;
            }
            return sum % 10 == 0;
        }
    }
}