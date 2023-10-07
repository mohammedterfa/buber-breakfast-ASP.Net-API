using BurberBreakfast.Models;
using BurberBreakfast.ServicesErrors;
using ErrorOr;

namespace BurberBreakfast.Services.Breakfasts;

public class breakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfacts = new();
    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfacts.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfacts.Remove(id);
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfacts.TryGetValue(id, out var breakfast)){
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public void UpsertBreakfast(Breakfast breakfast)
    {
        _breakfacts[breakfast.Id] = breakfast;
    }
}