using Notes.DAL.DbContext;
using Notes.BLL;
using Ninject.Modules;
using Ninject.Extensions.NamedScope;
using System.Configuration;
using Notes.BLL.Services;

namespace Notes.DI
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            Bind<INotesDbContext>().To<NotesDbContext>().InSingletonScope().
                WithConstructorArgument("connectionString", connectionString);
            Bind<IBusinessLogic>().To<BusinessLogic>();
            Bind<IUserService>().To<UserService>();
            Bind<INoteService>().To<NoteService>();
            Bind<ICategoryService>().To<CategoryService > ();
            Bind<INoteReferenceService>().To<NoteReferenceService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IUserRoleService>().To<UserRoleService>();
        }
    }
}
