

Add-Migration adv-001 -Context AdvertismentDbContext

Add-Migration InitialCreate -Project AdvertisementService.Persistence -StartupProject AdvertisementService.Api

Update-Database -Project AdvertisementService.Persistence -StartupProject AdvertisementService.Api -Context AdvertismentDbContext




Add-Migration InitialCreate -Project IdentityService.Persistence -StartupProject IdentityService.Api -Context ApplicationUserDbContext

Update-Database -Project IdentityService.Persistence -StartupProject IdentityService.Api -Context ApplicationUserDbContext
