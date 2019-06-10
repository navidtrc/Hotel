using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using InternshipHMSModels.Models;
using InternshipHMSDataAccess;

namespace InternshipHMSService
{
    public static class DefaultSetting
    {
        public static void CreateRoleUserDefaults()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("066e5a40-54ca-4c02-b7e3-ac3f6163e25d")) // SuperAdmin
                    roleManager.Create(new IdentityRole() { Name = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d" });
                if (!roleManager.RoleExists("b7ea9bd0-f597-4abe-8238-a9b94077cba6")) // Admin
                    roleManager.Create(new IdentityRole() { Name = "b7ea9bd0-f597-4abe-8238-a9b94077cba6" });
                if (!roleManager.RoleExists("3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")) // Employee
                    roleManager.Create(new IdentityRole() { Name = "3091bb05-4af6-4c41-b9e0-2b1fb9607a7e" });
                if (!roleManager.RoleExists("95a75089-f400-4e40-ab94-def3626f5374")) // Customer
                    roleManager.Create(new IdentityRole() { Name = "95a75089-f400-4e40-ab94-def3626f5374" });
                if (!roleManager.RoleExists("d5b194e0-b0e7-44a0-bc11-dbbab46ddd2a")) // Anonymous
                    roleManager.Create(new IdentityRole() { Name = "d5b194e0-b0e7-44a0-bc11-dbbab46ddd2a" });
                if (context.Employee_List.FirstOrDefault(f => f.EmployeeCode == "1") == null)
                {
                    Employee emp = new Employee()
                    {
                        FirstName = "Navid",
                        LastName = "Tafreshi",
                        NationalCode = "0440579899",
                        EmployeeCode = "1"
                    };
                    context.Employee_List.Add(emp);
                    context.SaveChanges();

                    ApplicationUser adminUser = new ApplicationUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        SerialNumber = Guid.NewGuid().ToString(),
                        AplicationUser_Person = emp
                    };
                    var result = userManager.Create(adminUser, "111111");
                    if (result.Succeeded)
                        userManager.AddToRole(adminUser.Id, "066e5a40-54ca-4c02-b7e3-ac3f6163e25d");
                }
            }
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {



        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
