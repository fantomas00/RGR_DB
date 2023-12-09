using Rgr.Domain.Pharmacies;

namespace Rgr.Console.Controllers;

public class PharmacyController : Controller<Pharmacy>
{
    public PharmacyController(IPharmacyRepository repository) : base(repository)
    {
    }
}
