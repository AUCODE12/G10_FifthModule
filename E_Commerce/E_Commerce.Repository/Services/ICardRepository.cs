using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace E_Commerce.Repository.Services;

public interface ICardRepository
{
    Task<long> InsertCardAsync(Card card);
    Task<List<Card>> SelectCardsByCustomerIdAsync(long customerId);
    Task<Card> SelectSelectedCardByCustomerIdAsync(long customerId);
    Task AssignCardAsSelectedAsync(long cardId);
    Task AssignCardAsNotSelectedAsync(long cardId);
    Task DeleteCardAsync(long cardId);
}