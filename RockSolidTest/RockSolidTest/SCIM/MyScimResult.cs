using Rsk.AspNetCore.Scim.Enums;
using Rsk.AspNetCore.Scim.Results;

namespace RockSolidTest.SCIM
{
    public class MyScimResult<T> : ScimResult<T>, IScimResult<T> where T : class
    {
        public MyScimResult(
            T resource,
            ScimResultStatus status,
            ScimStatusCode? suggestedStatusCode,
            params string[] errors) : base(resource, status, suggestedStatusCode, errors)
        {
        }
    }
}