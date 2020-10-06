using System;
using System.Threading.Tasks;

public interface IRepository
{
    Task<Listing[]> GetAll();
    Task<Listing> NewHighScore(Listing newListing);
    Task<Listing> Delete(Guid id);
}