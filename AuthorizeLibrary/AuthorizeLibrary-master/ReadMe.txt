* Install the following puckages
	- NuGet\Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design 
	- NuGet\Install-Package Microsoft.AspNetCore.Identity.UI
	- NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools

* In Program.cs write the following code
	- var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
		?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

	- ApplicationDbContext.DBConnctionString = connectionString;

	- builder.Services.AddDbContext<ApplicationDbContext>(/*options =>
		options.UseSqlServer(connectionString)*/);

	- builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	- builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultUI(); 

* Donte forget add Identity viwe