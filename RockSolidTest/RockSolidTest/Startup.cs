using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RockSolidTest.SCIM;
using Rsk.AspNetCore.Scim.Configuration;
using Rsk.AspNetCore.Scim.Models;
using Rsk.AspNetCore.Scim.Validators;

namespace RockSolidTest
{
    public class Startup
    {
        private readonly string _lincenceName = "Demo";

        public string LicenseKey =
            "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjEtMDgtMjlUMDE6MDA6MDUuODQ1MDYyOSswMTowMCIsImlhdCI6IjIwMjEtMDctMzBUMDA6MDA6MDUiLCJvcmciOiJERU1PIiwiYXVkIjo4fQ==.n5Wg3Dh2Fr3U/oGzS7aQup5huQsH53B6Z8/xKXZCYMCjJd1o6qA2NUSbufE7kdOcDDBH8bdlDRu05AOopzbsMYPoakv2oitlim/ZNaY5RaAPUfRqR5g2ewEVpKlVzGVwKlyEmft+py8G72gN2tmCI6exe3bCdCSKLndVZ+EN5WCtwmbitGFx6Bl6Z1NcDHpOFDylpkzWhDPuH17gc7qmZpkuC69JhlobzCf3RXWGbhYbz67gzdVGY/CJOUrMbOPIuc7MnEl0n7IQxUVI/Nnipaxcawma6/MiehdeFczwNUeMA3SDV0Q6yxkbyuOfo8ZuxZf/LVLgetqcXxCszI6xnh0jhjMygmHY5r0urnN5r4/bmx1CxbQvcO3ufJy/8SI+lhTc6r0j8bOys5R1ZZEuQpRkw+fzgMf/FTIbnmzrYIsIhDcyDOnEZDkDe/BNtpHBbSVzaJnIdv18hBfj4q4pmU7AOiN2nMZ/2VerIC+KZM7QwNFPC0hVZHtZskzfuoBGvKDv02YWLvT37ZjRVM5cajpvVlGyLtguuNJ8HrJf0sjRRt3jIWGNB9Xkfd3A8CzvNtcNfl0CkWzlRhYw+NxaIdZ0o113LFYiWBhUS31ti+86kTEXnWx6o6ZB9L2sML+9EdV7yVoEKwKHEsgatKIQqQv+OqD5l7rf1le7BVml+X8=";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddScimServiceProvider("/scim",
                    new ScimLicensingOptions {LicenseKey = LicenseKey, Licensee = _lincenceName})
                .AddResource<User, UserStore, UserValidator>("urn:ietf:params:scim:schemas:core:2.0:User", "Users")
                .AddResourceExtension<User, Core2EnterpriseUserExtention>(
                    "urn:ietf:params:scim:schemas:extension:enterprise:2.0:User")
            .AddResourceExtension<User, MyCustomExtension>(
                "urn:ietf:params:scim:schemas:extension:enterprise:2.0:MyCustomExtension");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

            app.UseScim();
        }
 
    }
}