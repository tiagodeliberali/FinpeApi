using FinpeApi.Utils;
using System.Collections.Generic;

namespace FinpeApi.Banks
{
    public interface IBankRepository
    {
        IReadOnlyList<Bank> GetList();
    }
}
