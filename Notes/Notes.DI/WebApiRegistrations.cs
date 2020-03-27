using Ninject.Web.WebApi;
using Notes.BLL;
using Notes.BLL.Services;
using Notes.DAL.DbContext;
using System.Configuration;

namespace Notes.DI
{
    public class WebApiRegistrations: WebApiModule
    {
        public override void Load()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            Bind<INotesDbContext>().To<NotesDbContext>().
                    WithConstructorArgument("connectionString", connectionString);
            Bind<IBusinessLogic>().To<BusinessLogic>();
            Bind<IUserService>().To<UserService>();
            Bind<INoteService>().To<NoteService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<INoteReferenceService>().To<NoteReferenceService>();
        }
    }
}
