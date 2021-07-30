using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rsk.AspNetCore.Scim.Enums;
using Rsk.AspNetCore.Scim.Models;
using Rsk.AspNetCore.Scim.Results;
using Rsk.AspNetCore.Scim.Stores;

namespace RockSolidTest.SCIM
{
    public class UserStore : IScimStore<User>
    {
        private readonly string FilePath = "users.txt";

        public Task<IEnumerable<(bool Exists, string Id)>> Exists(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IScimResult> Delete(string id, string resourceSchema)
        {
            throw new NotImplementedException();
        }

        public async Task<IScimResult<User>> Add(User resource, IEnumerable<ScimExtensionValue> scimExtensions,
            string resourceSchema)
        {
            if (File.Exists(FilePath))
            {
                var fileLines = await File.ReadAllLinesAsync(FilePath);
                foreach (var line in fileLines)
                {
                    var user = JsonConvert.DeserializeObject<User>(line);
                    if (user.UserName == resource.UserName) throw new Exception("User name exists");
                }
            }

            resource.Id = Guid.NewGuid().ToString();
            var serializedUser = JsonConvert.SerializeObject(resource);
            await using (var writer = File.AppendText(FilePath))
            {
                await writer.WriteLineAsync(serializedUser);
            }

            var result =
                new MyScimResult<User>(resource, ScimResultStatus.Success, ScimStatusCode.Status201Created);
            return result;
        }


        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IScimResult<User>> Update(User resource, IEnumerable<ScimExtensionValue> scimExtensions,
            string resourceSchema)
        {
            throw new NotImplementedException();
        }

        public Task<IScimResult<IEnumerable<ScimExtensionValue>>> GetExtensionsForResource(string resourceId,
            string resourceSchema)
        {
            throw new NotImplementedException();
        }
    }
}