using E_Commerce.Dal;
using E_Commerce.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CardRepository : ICardRepository
{
    private readonly MainContext MainContext;

    public CardRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public Task AssignCardAsNotSelectedAsync(long cardId)
    {
        throw new NotImplementedException();
    }

    public Task AssignCardAsSelectedAsync(long cardId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCardAsync(long cardId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Card>> SelectCardsByCustomerIdAsync(long customerId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertCardAsync(Card card)
    {
        throw new NotImplementedException();
    }

    public async Task<Card?> SelectSelectedCardByCustomerIdAsync(long customerId)
    {
        var card = await MainContext.Cards.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.SelectedForPayment == true);

        return card;
    }
}
