using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rsk.AspNetCore.Scim.Enums;
using Rsk.AspNetCore.Scim.Models;
using Rsk.AspNetCore.Scim.Results;
using Rsk.AspNetCore.Scim.Validators;
using ScimUser = Rsk.AspNetCore.Scim.Models.User;


namespace RockSolidTest.SCIM
{
    public class UserValidator : IScimValidator<User>
    {
        public Task<IScimResult<User>> ValidateUpdate(string resourceAsString, string schema)
        {
            throw new NotImplementedException();
        }

        public Task<IScimResult<ScimUser>> ValidateAdd(string resourceAsString, string schema)
        {
            var user = JsonConvert.DeserializeObject<ScimUser>(resourceAsString);

            var schemaResult = HasExpectedSchemas(user, schema);

            if (schemaResult.Status == ScimResultStatus.Failure)
            {
                return Task.FromResult(schemaResult);
            }

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                var error = ScimResult<ScimUser>.Error(ScimStatusCode.Status400BadRequest,
                    "Username is required on User");

                return Task.FromResult(error as IScimResult<ScimUser>);
            }

            var success = ScimResult<ScimUser>.Success(user);

            return Task.FromResult(success as IScimResult<ScimUser>);
        }

        private IScimResult<ScimUser> HasExpectedSchemas(ScimUser user, string schema)
        {
            if (user.Schemas == null || !user.Schemas.Any())
            {
                return ScimResult<ScimUser>.Error(ScimStatusCode.Status400BadRequest,
                    "Resource doesn't contain any schemas");
            }

            var hasExpectedSchema = user.Schemas.Contains(schema);

            if (!hasExpectedSchema)
            {
                return ScimResult<ScimUser>.Error(ScimStatusCode.Status400BadRequest,
                    "Resource doesn't contain expected schema");
            }

            return ScimResult<ScimUser>.Success(user);
        }
    }
}