using Notes.BLL.DTOModels;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using System;
using System.Web.Security;
using Notes.BLL.Services;

namespace Notes.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly INotesDbContext _db;        
        public BusinessLogic(INotesDbContext db, IUserService users, ICategoryService categories, 
            INoteService notes, INoteReferenceService noteReferences, IRoleService roles, IUserRoleService userRoles)
        {
            _db = db;
            Roles = roles;
            Users = users;
            UserRoles = userRoles;
            Categories = categories;
            Notes = notes;
            NoteReferences = noteReferences;

        }

        public IUserService Users { get; }
        public INoteService Notes { get; }
        public ICategoryService Categories { get; }        
        public INoteReferenceService NoteReferences { get; }

        public IRoleService Roles { get; }

        public IUserRoleService UserRoles { get; }
    }
}
