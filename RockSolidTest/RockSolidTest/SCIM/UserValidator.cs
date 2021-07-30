using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rsk.AspNetCore.Scim.Enums;
using Rsk.AspNetCore.Scim.Models;
using Rsk.AspNetCore.Scim.Results;
using Rsk.AspNetCore.Scim.Validators;

namespace RockSolidTest.SCIM
{
    public class UserValidator : IScimValidator<User>
    {
        public Task<IScimResult<User>> ValidateUpdate(string resourceAsString, string schema)
        {
            throw new NotImplementedException();
        }

#pragma warning disable 1998
        public async Task<IScimResult<User>> ValidateAdd(string resourceAsString, string schema)
#pragma warning restore 1998
        {
            var user = JsonConvert.DeserializeObject<User>(resourceAsString);
            foreach (var extension in user.Schemas)
            {
                var txt = extension.Value;
            }
            Task.Delay(1);
            return new MyScimResult<User>(user, ScimResultStatus.Success, ScimStatusCode.Status200Ok);
        }
    }
}