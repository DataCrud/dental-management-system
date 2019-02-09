﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using DM.AuthServer.Models;
using DM.AuthServer.Repository;
using DM.Service;
using DM.Service.Contacts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DM.AuthServer.Service
{
    public interface IPermissionService : IBaseService<SecurityModels.Permission>
    {              
        bool CheckPermission(Models.RequestModels.PermissionRequestModel model);
        List<SecurityModels.Permission> GetListById(string request);
        bool AddList(List<SecurityModels.Permission> models);
    }




    public class PermissionService: BaseService<SecurityModels.Permission>, IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IResourceRepository _resourceRepository;

        public PermissionService(IPermissionRepository repository, IResourceRepository resourceRepository) : base (repository)
        {
            _repository = repository;
            _resourceRepository = resourceRepository;
        }     


        public bool CheckPermission(Models.RequestModels.PermissionRequestModel model)
        {            
            var resource = _resourceRepository.CheckResource(model.Route);

            if (resource.IsPublic) return true;


            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());

            bool isPermitted = false;

            foreach (var role in user.Roles)
            {
                model.RoleId = role.RoleId;
                model.ResourceId = resource.Id;

                var permission = _repository.CheckPermission(model);

                if (permission != null)
                    isPermitted = true;
            }

            return isPermitted;
        }        
       

        public List<SecurityModels.Permission> GetListById(string request)
        {
            return _repository.GetListById(request).ToList();
        }

        public bool AddList(List<SecurityModels.Permission> models)
        {            
            return _repository.AddList(models);
        }
    }
}