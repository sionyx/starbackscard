using Starbucks.Models;
using System.Linq;
using Windows.Security.Credentials;

namespace Starbucks.Services
{
    public class CardsStorage
    {
        private const string Resource = "cardinfo";

        public void StoreCardData(CardData cardData)
        {
            var voult = new PasswordVault();
            voult.Add(new PasswordCredential(Resource, cardData.Number, cardData.Code));
        }

        public void DeleteCardData(CardData cardData)
        {
            var voult = new PasswordVault();
            var cardCred = voult.Retrieve(Resource, cardData.Number);
            if (cardCred == null) return;
            voult.Remove(cardCred);
        }

        public CardData GetCardData()
        {
            var voult = new PasswordVault();
            var list = voult.RetrieveAll();

            if (list.Count <= 0) return null;

            var firstCred = list.First();
            var cardCred = voult.Retrieve(Resource, firstCred.UserName);
            return new CardData { Number = cardCred.UserName, Code = cardCred.Password };
        }
    }
}
