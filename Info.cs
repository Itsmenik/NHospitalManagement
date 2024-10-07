here i the connection string from the office   


    // goto:#Migration Error 


    Creating Migrationg ( EntityFrameworkCore\Add-Migration mg1) 



--> here is the connection string for the local (Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;) 


agar kabhi reference n add ho to class lib ke uper do bar click karo aur 
waha jake aise add kar dena h 
  <ItemGroup>
    <ProjectReference Include="..\Hospital.Model\Hospital.Model.csproj" />
  </ItemGroup>


Create project in Asp.net MVC then we have to create the  -->  

then Add Class Library 
(Repositery)
(Services) 
(ViewModel)
(Model) 
(Utility) 


Then Add file class _-> ApplicationDbContext Repositery mei --> Banyenge
which is inherited from the --> ApplicationDbContext 


which inherit IdentityDbContext(ye user authentication role bhi provide karta h ) --> (Package fro
other package --> 
Microsoft.EntityFramework.SqlServer; 
                         .Tools;
) 
here is the -->  
(
IdentityDbContext is a specialized DbContext that includes all the necessary schemas for ASP.NET Core Identity, which provides a ready-made solution for user management, authentication, and authorization. This context already has the necessary configurations for handling user roles, logins, and claims.
Key Characteristics of IdentityDbContext:
Inherits from DbContext, but pre-configured to handle ASP.NET Core Identity.
Comes with built-in DbSet properties for user-related entities like IdentityUser, IdentityRole, IdentityUserRole, etc.
You can extend it to include your own entity sets while still keeping the user management functionality.
)

==> Application mei DBcontext banae ke bad appsettting.json mei humko connectionstring dalni --> 
Aur mention Program.cs mei mention karna hai -->  

(Error 
System.InvalidOperationException: 'The service collection cannot be modified because it is read-only.' in the builder.Dbcontext 
for that we have to put the (dbcontext part before the  var app = Builder)
 
 like this --> 
 builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

var app = builder.Build(); 

)
Scaffolding -- for the Identity -->(Righ click on the Web project --> New then --> New Scaffold item -->
select Identity sidebar --> click on  the override all  (## Below we have to selecte the ApplicattionDbContext class) -->  

It will generate the file name Area in which we have Data and Pages 
) 

**Scaffolding Meaning -->(In the context of a Visual Studio Code (VS Code) project, a scaffold item refers to a pre-built template or structure that you can generate to quickly set up parts of your application. Scaffolding is commonly used in web development frameworks like ASP.NET Core to create standard files and code needed for specific functionalities, such as Identity, controllers, views, and more.

Common Types of Scaffolded Items in ASP.NET Core:
Identity: Generates the necessary files for user authentication, such as login, registration, and user management views and controllers.
Controllers: Creates a controller class with actions for handling HTTP requests, often along with views.
Views: Generates Razor views for displaying data and user interfaces.
)


(
Error --> while creating the new Scaffold Item in the Hopmng_Web to solve this we just 
add the package and path in the we have done this --> (
Add the online reference: Tools > nuget package manager > package manager settings > Package Sources

Add source: https://api.nuget.org/v3/index.json

) 

______________________________________________ 
Delete the data folder from the Identity Folder 
then in the Program.cs we have to remove the  


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
we have to romve from  the option --> true part which require the -->  ConfirmationAccount


------------------------------------------------------------------------------ 
Next step To Create the Roles for that add ---> Class in the Hospital.Utility

namespace Hopital.Uitility
{
    public static  class WebsiteRoles // static class so we can easily acess without creating the Instance 
    {
        public const string WebSite_Admin = "Admin";
        public const string WebSite_Patient = "Patient";
        public const string WebSite_Doctor = "Doctor";
    }
}
-_______________________________ 
Create a (Interface) in the Utility that is for the Defualt Roles   
here is the interface with construcutor --> 


namespace Hopital.Uitility
{
    public  interface IDefaultUserIntializer
    {
         void Initialize();
    }
}
 
and then we have  to create the class that is implmentig this method  

---> here is the class  
these are three class that is inherited from the ApplicationUser and
    private  UserManager<IdentityUser> _userManager ; // got the error we have to microsoft.store and then (Use Local version)
    private  RoleManager<IdentityRole> _roleManager ; //RoleManager Inherited from IdentityRole  _roleManage is the instance of these  class 





    private ApplicationDbContext _Context; 
     


----> select this upper all and then click on the create the contructor 

public DefaultUserIntializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _Context = context;
        } 

________________________________________________________________ 
here is the implementation of the intializer that we made in the interface 

   public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate(); // if we are using the migration 
                    //and if the Migration count is bigger than 0 then first w hve
                    // migrate in the database  

                }

            }
            catch (Exception ex) {
                throw ex;
            }
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebSite_Admin).GetAwaiter().GetResult()) { // if the role is not exist (GetAwaiter method basically used for the Asynchrounous ke liye GetResult() ke liye
            .GetAwaiter().GetResult() blocks the asynchronous call and waits for it to complete synchronously.
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Admin)).GetAwaiter().GetResult() /// then we are going to create the role 
                  _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Patient)).GetAwaiter().GetResult()
                   _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Doctor)).GetAwaiter().GetResult()

                  _userManager.CreateAsync(
                      new ApplicationUser // which is coming from the model    public class ApplicationUser : IdentityUser (this class has many build in property -> Id , UserName,Email,PasswordHash,SecurityStamp,PhoneNumber,TwoFactorEnabled

                      {
                          UserName = "Nikhil",
                          Email = "nik@gmail.com"
                      }, "Nikhil@123"
                      ).GetAwaiter().GetResult();
                var Appuser = _context.ApplicationUser.FirstOrDefault(x => x.Email == "Nik@gmail.com");// find that the user is exist or not
                if (Appuser != null) {
                    _userManager.AddToRoleAsync(Appuser, WebsiteRoles.WebSite_Admin).GetAwaiter().GetResult();
                    // here we are adding that role 
                }
            }
        } 

__________________________________________________________________________________________ 
Now the end of the Identity framework from here 
-->We are going to create the 
Generic Repositery 
-> Unit Of Work Pattern 
--> Create Areas 
-->Configure Program.cs 
-->Apply First Migration 
-->First Time to Run the Application 
------------------> 
First we are going to create the Generic Repositery(((there is an interface) where we a default method and common method(getAll,  that is used by 
the application ) 
here is the entity class that is made in the Interface folder  with the name  IGenricReopositery 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.Repositery.Interface
{
    public interface IGenericRepository<T> : IDisposable // here we have to pass the entity in which foprm the data is coming
     //IDisposable class is class that free up resource when the interface is not inthe use 
    {
        IEnumerable<T> GetAll( 

            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        void Add(T entity);
        Task<T> AddAsync(T entity);

        void Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(T entity);
        Task<T> DeleteAsync(T entity);
    }
}

here is the simple  example how it works --> 

public class IntRepository : IGenericRepository<int>
{
    private List<int> _numbers = new List<int> { 1, 2, 3, 4 };

    public IEnumerable<int> GetAll()
    {
        return _numbers;
    }

    public int GetById(object id)
    {
        int intId = (int)id;
        return _numbers.FirstOrDefault(n => n == intId);
    }

    public void Add(int entity)
    {
        _numbers.Add(entity);
    }

    public void Delete(int entity)
    {
        _numbers.Remove(entity);
    }

    public void Update(int entity)
    {
        // Example: We will replace the first item with the new entity (just for demonstration)
        if (_numbers.Contains(entity))
        {
            int index = _numbers.IndexOf(entity);
            _numbers[index] = entity;
        }
    }
}
----------------------------------------------------------- 
Now the implementation of this generic interface Implementation  


here is implementation of our Generic Class 

namespace Hospital.Repositery.Implementation
{ 
    public class GenricRepositery<T>:IDisposable, IGenericRepository<T> where T: class 
 // here we have to right clikc on the  IDisposable and then select for the Implement the Interface 
 similarly like  On IGenricRepositery Right click on the button and select the Implement the interface 




    {
        private readonly ApplicationDbContext _context; 
        internal DbSet<T> dbSet;//this for the intracting with the table where we have to pass the Table 

        public GenricRepositery(ApplicationDbContext context)
        {
            _context = context; 
            dbSet = _context.Set<T>(); 

            // what is the Meaning of this --> dbSet = _context.Set<User>();
This makes dbSet represent the "Users" table in the database. 
Now you can perform operations like 
dbSet.Add(user), dbSet.Remove(user), dbSet.Find(id) )


        }

        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity); 
            
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }


        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) {

                    _context.Dispose();
                }
            } 
            this.disposed = true;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            } 
            foreach(var includeProperty in 
                includeProperties.Split(new char[] {','} , StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            } 
            if(orderBy != null)
            {
                return orderBy(query).ToList();
            } 
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
           dbSet.Attach(entity);
           _context.Entry(entity).State = EntityState.Modified;
        }

      
    }
}
_______________________________________________________________________ 
Now we are going to create the UNIT OF WORK (is a kind of IRepositery class on Operation Repositer) 
similarly like the Repo we have to create the --> Interface and then implementation 


अब दोनों ऑपरेशन अलग-अलग रिपॉजिटरी (PatientRepository और DoctorRepository) पर होते हैं। 
यदि मरीज का डेटा सफलतापूर्वक अपडेट हो जाता है, लेकिन डॉक्टर के डेटा को अपडेट करते समय कोई त्रुटि आ जाती है, 
तो आपके पास एक समस्या हो सकती है, जहां मरीज का डेटा अपडेट हो गया है, लेकिन डॉक्टर का डेटा अधूरा है।

इस तरह की असंगतियों से बचने के लिए Unit of Work का उपयोग किया जाता है। 
यह क्या करता है? यह एक ट्रांज़ैक्शन को ट्रैक करता है, जिसमें सभी ऑपरेशन होते हैं।
अंत में यह सुनिश्चित करता है कि सभी ऑपरेशन सफल हुए हैं, तभी यह परिवर्तन को डेटाबेस में सेव करता है।

here is interface --> 
namespace Hospital.Repositery.Interface
{
    internal interface IUnitOfWork
    { 
        IGenericRepository<T> GenericRepository<T>() where T : class;

        void Save(); 
    }
}
and here is the implmentation of the interface  --> 


namespace Hospital.Repositery.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    { 
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) // here is the contructor 
        {
          _context = context;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        { 
            IGenericRepository<T> repository = new GenricRepositery<T>(_context);
            return repository; 
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

-------------------------------------------------- 
Now we have to add in the (Area that is created during the Identity MVC) --> Mvc area (Righ click On Areafolder Using Add --> New Scaffolding --> Common -> MVC area ) 
Add the three MVC --> Admin , Doctor , Pateint 


put the scaffolding name Admin --> Under that we have to delete (Data, Model folder) 

after creatin the scaffolder we have to delete the file from the area folder 
what is scaffolding why we need this  
basically it is kind of method through which we can generate the code --> 
In projects using the Repository Pattern, you typically need to interact with the database via Entity Framework. Scaffolding can generate the entity classes (models) and DbContext class based on an 
existing database schema,
making database integration much easier)  

After creating the Scaffolding --> we have to delete the data bad model in every MVC 
-->  
---------------------------------------
Now in the Patient MVC in Controller folder we have to 
create the Controller (pur defuals Name HomeController) 
Now in the HomeControler for the Index create the --> View 

---->Now Copy the ViewImpor and ViewStart form the View and paste 
it into the Area folder 


------------------------------------------------------------------------ 
Now after that we have to configure the Program.cs file   

first we have to add these two services --> 
builder.Services.AddIdentity<IdentityUser ,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
First we have to invoked the Services 


builder.Services.AddScoped<IDefaultUserIntializer, DefaultUserIntializer>(); then DbInitializer 
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();//then we have to onvoke the unit of work 

then we have to call the Dataseeding method --> 

void DataSedding()
// we are creating this method to call the Initializer where we have the Default Admin and Role is exist 

{
    using(var scope = app.Services.CreateScope()) // with the help of Services
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDefaultUserIntializer>(); 
        dbInitializer.Initialize();
    }
}  

//then we have configure the Map Routing also  
just add the Area in this --> 
 pattern: "{Area=Patient}/{controller=Home}/{action=Index}/{id?}");


/// Then we have to create the Map for the User Page 
we have to add this --> 
after the app.UserAuthorization() 
app.MapRazorPages(); 

then in the Service we have to invoke the user Page Service also 

builder.Services.AddRazorPages();

In this page we got some error for that we change the data to the local and 
then we have some change tin the ApplicationUser class 

public class ApplicationUser : IdentityUser<string> // here we installed the new type and the n
    {


    then 
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); // use this AddDefualtTokenProvider 

___________________________________________________________________________________________ 
After configuring the Program.cs file 
we have to  Migration to Insert some data in he Database  

To the Migration we have to write like this 
---> add-migration initial(migrationanme)  in the Console in upper select --> Hospital.Repositer instead of Default project  
in which we also get some error  
(We got the error while creating the Migration ) 
the first reason is the ConntectionString 
 ERRROR
for that we have to install the EntityFrameworkcore and then  

(then we have to write this command in the Console _-> EntityFrameworkCore\Add-Migration InitialCreate 
--then this command to update the db --> EntityFrameworkCore\Update-Database )

after the creation of the migration there is Several Table Will create  inthe Database  
--> related to the login user and other table also 
---------------------------------------------------------------------- 
Then run the application there is an errro will occure that is the Related to Routing 
--> In we are rendering the Patient page bydefualt but the --> we did not defined the area inthe Patient page 

So in the Patient page --> So we can the Router can found that we are taling about which patient page 
[Area("Patient")] 
_____________________________________________________________________________________________________________________ 
after all that we have to into the Layout.cshtml and in that we have to write 
below the ending of the <ul> tag  


</ul> 
 <partial name ="_LoginPartial"/>(this is useed to  render the page below)  

Error 
_->InvalidOperationException: No service for type 'Microsoft.AspNetCore.Identity.UserManager`1[Microsoft.AspNetCore.Identity.IdentityUser]' has been registered. 

So for that we have to paste  
--> this thing on the place of @inject UserManager<IdentityUser> userManager 
--> to @inject UserManager<MyUserStore> userManager

--------------------------------------------------------------------------------- 
After that we have to implment a classs name Emailsender which is Inherted from IEmailSender 
then Just righ click --> (find and install the latest versiong) 

after that we --> righ click on the IEmailSender and then choose Implement Interface  
and then add as --> depandencies in the Program.cs 
___________________________________________________________________________ 

Everything is Done Now It's Time for the Model 

MODEL DESIGN -->  
we have to design the model in all the rellated  

After creating the Models in Our Model  we Create the Model in the ViewModel   

1. Separation of Concerns
ViewModel is designed to provide data to the View without exposing the complexity of the underlying Model. By creating a model in the ViewModel, you decouple the business logic (Model) from the presentation logic (View), keeping each layer focused on its responsibilities.
2. Data Shaping for Views
The ViewModel often contains data that is specifically formatted or structured for display purposes. In many cases, a Model object (representing the data layer) might not have the necessary structure or fields required by the View, so the ViewModel can shape the data to fit the View's needs.



 
we are going to create Models in the ViewModel 

here is the code of the VeiwModel 

using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class HospitalInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }

        public HospitalInfoViewModel()
        { 

            
        }
        public HospitalInfoViewModel(HospitalInfo modelmapping)
        { 
            Id = modelmapping.Id;
            Name = modelmapping.Name;
            Type = modelmapping.Type;
            City = modelmapping.City;
            PinCode = modelmapping.PinCode;
            Country = modelmapping.Country;
                
        }
        public HospitalInfo ConvertViewModel(HospitalInfoViewModel model)// yejab hum insertiong ke time data user se lenger to 
            // HospitalInfo mei convert karna padega 
        {
            return new HospitalInfo
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                City = model.City,
                PinCode = model.PinCode,
                Country = model.Country

            };

        }
    }
}
_____________________________________________________________________________________________________  
Now we are going to create PageResult in Hospital.Utility so we can add the paggin feature in our code 
here is the   
    using NPOI.SS.Formula.Functions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Hopital.Uitility
    {
        public class PageResult
        {
            public PageResult() { }
            public List<T> Data { get; set; }
            public int TotalItems { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }
    }  

---- ________________________________________________________________-
Now we are going to the Service where we write the comman method(method that is used by the every page ) that is Useable by the Hospital model 
so here we  are going to add the  (Interface)

here is  the code 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopital.Uitility;
using Hospital.ViewModel;
namespace Hospital.Services
{
    internal interface IHospitalInfo
    {
        PageResult<HospitalInfoViewModel> GetAll(int PageNumber, int pageSize);

        HospitalInfoViewModel GetHospitalById(int HospitalId);  all the kind of operation we are going to perfomr in the in Hospital mdel we will define here 
        void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void DeleteHospitalInfo(int id);



    }
}

________________________________________________________________________________________________________ 

After that we have to ceating the service interface we are going to implement that interace  
    into the concrete class  
    ********************VVI**************************
here is the code  now go the the Implemented  here is then whole code where we implemente the common method and 
using Genric class for insertin updation and something --> 
    and uisng the Unit of work for the save that provide the Sequence Transaction 



using Hospital.Model;
using Hospital.Repositery.Interface;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopital.Uitility;

namespace Hospital.Services
{
    public class HospitalInofServices : IHospitalInfo
    {
        private IUnitOfWork _unitOfWork;

        public HospitalInofServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResult<HospitalInfoViewModel> GetAll(int PageNumber, int PageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;
                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().Skip(ExcludeRecords).Take(PageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }

            catch (Exception ex)
            {
                throw;
            }

            var result = new PageResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize

            };
            return result;

        }
        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
        {
            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }

        void IHospitalInfo.DeleteHospitalInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitOfWork.Save();
        }



        HospitalInfoViewModel IHospitalInfo.GetHospitalById(int HospitalId)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(HospitalId);
            var vm = new HospitalInfoViewModel(model);
            return vm;
        }

        void IHospitalInfo.InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();
        }

        void IHospitalInfo.UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var ModelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);

            ModelById.Name = hospitalInfo.Name;
            ModelById.City = hospitalInfo.City;
            ModelById.PinCode = hospitalInfo.PinCode;
            ModelById.Country = hospitalInfo.Country;
            _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
            _unitOfWork.Save();
        }
    }
}

 
------------------------------------ 
Now we are going to do something in the Area Section 
    So we have to crate a controller inthe Admin Area (empty controller ) and then  we have to crete the 
    migration (migration create karte samay humbe --> Hospital.Repositery ka use kara tha jisme 
    hamare pass ApplicationDbcontext jike andar (identityUser) parent class th\a on the basis of that the table will create (anme of the table and all the other stuff )  
     
 we got the error  while creating the migration first 

Migration Error
// #Error    we go the error while creating the Error 
// #EntityFrameworkCore\Add-Migration  To add the migration  
At the end we got the error of foreign key like the   
    there is a change in Appointement --> 

    {
    public class Appointments
{
    // Change these to the appropriate type, usually string for user IDs
    public string DoctorId { get; set; }  Becuase  ApplicationUser mei Dono hai to doctor ka appointment hoga y customer 
    public string PatientId { get; set; }

    public int Id { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }

    // If you do not want to map these navigation properties, use [NotMapped]
    [NotMapped]
    public ApplicationUser Doctor { get; set; }

    [NotMapped]
    public ApplicationUser Patient { get; set; }
}
}

then we got the error in the Migration --> 


    migrationBuilder.CreateTable(
    name: "PatientReport",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Diagnose = table.Column<string>(type: "nvarchar(max)", nullable: false),
        MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
        DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
        PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_PatientReport", x => x.Id);
        table.ForeignKey(
            name: "FK_PatientReport_ApplicationUser_DoctorId",
            column: x => x.DoctorId,
            principalTable: "ApplicationUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);  // Change to NoAction
        table.ForeignKey(
            name: "FK_PatientReport_ApplicationUser_PatientId",
            column: x => x.PatientId,
            principalTable: "ApplicationUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);  // Change to NoAction
    });

then again we got the error in the TestPrice  

    here is then change 
    migrationBuilder.CreateTable(
    name: "TestPrices",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        TestCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        LabId = table.Column<int>(type: "int", nullable: false),
        BillId = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_TestPrices", x => x.Id);
        table.ForeignKey(
            name: "FK_TestPrices_Bills_BillId",
            column: x => x.BillId,
            principalTable: "Bills",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction); // Change to NoAction
        table.ForeignKey(
            name: "FK_TestPrices_Labs_LabId",
            column: x => x.LabId,
            principalTable: "Labs",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction); // Change to NoAction
    });


then the errro gone and we sucessfully able to create the migration  and all the table create in the db  
    that is definged inthe ApplictionDbContext
    
then we override the onModelCreation --> 

            protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Appointments>()
        .HasOne(a => a.Doctor) // Assuming a navigation property
        .WithMany() // Depending on the relationships defined
        .HasForeignKey(a => a.DoctorId)
        .OnDelete(DeleteBehavior.Cascade); // Cascading delete for DoctorId

    modelBuilder.Entity<Appointments>()
        .HasOne(a => a.Patient) // Assuming a navigation property
        .WithMany() // Depending on the relationships defined
        .HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.NoAction); // No cascading delete for PatientId
}


After all this we sucessfully able to create the table 