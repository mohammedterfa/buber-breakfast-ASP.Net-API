using BurberBreakfast.Models;
using ErrorOr;

namespace BurberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertBreakfast> UpsertBreakfast(Breakfast breakfast);
}