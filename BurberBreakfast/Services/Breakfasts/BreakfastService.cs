using BurberBreakfast.Models;
using BurberBreakfast.ServicesErrors;
using ErrorOr;

namespace BurberBreakfast.Services.Breakfasts;

public class breakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfacts = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfacts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfacts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfacts.TryGetValue(id, out var breakfast)){
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<Updated> UpsertBreakfast(Breakfast breakfast)
    {
        _breakfacts[breakfast.Id] = breakfast;

        return Result.Updated;
    }
}