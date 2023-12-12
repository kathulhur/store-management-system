using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Interfaces;

public interface IUpdateProductArgs
{
    string? Name { get; }

    decimal? CostPrice { get; }

    decimal? SellingPrice { get; }

    int? InStock { get; }
}
