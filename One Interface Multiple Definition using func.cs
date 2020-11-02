using GlobalServe.Core.StandardModels;
using GlobalServe.Core.ViewModels;
using GlobalServe.Data.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GlobalServe.Core.Services.Interfaces
{
    public abstract class BasketAbstract
    {
       
      
        protected readonly ApplicationUserService _userService;
        protected GlobalServeDbContext _dbContext;
        protected StandardApiResponse standardApiResponse;
        protected readonly ConfigurationService _configurationService;
        public BasketAbstract()
        {

        }
        public BasketAbstract(GlobalServeDbContext dbContext, ConfigurationService configurationService, ApplicationUserService userService)
        {
           
            _userService = userService;
            _configurationService = configurationService;
            _dbContext = dbContext;
            standardApiResponse = new StandardApiResponse();
        }
      
        public abstract bool AuthenticateItems(BasketItemRequestModel item);

        
    }

   
    public class CatalogItem : BasketAbstract
    {
        public CatalogItem(){}
        public CatalogItem(GlobalServeDbContext dbContext, ConfigurationService configurationService, ApplicationUserService userService) :base(dbContext, configurationService, userService)
        {

        }

        public override bool AuthenticateItems(BasketItemRequestModel item)
        {
            var authenticatePartItem = _dbContext.TblCatalogParts
                                                         .Where(x => x.CatalogPartId == item.CatalogPartId
                                                                  && x.CustomerId == _userService.CustomerId
                                                                  && x.IsActive == true)
                                                         .SingleOrDefault();

            if (authenticatePartItem != null)
                return true;
            else
                return false;
        }
    }
    public class StandardItem : BasketAbstract
    {
        public StandardItem()
        {

        }
        public StandardItem(GlobalServeDbContext dbContext, ConfigurationService configurationService, ApplicationUserService userService) : base(dbContext, configurationService, userService)
        {
                
        }
        public override bool AuthenticateItems(BasketItemRequestModel item)
        {
            var authenticateStandardItem = _dbContext.TblCatalogStandards
                                                        .Where(x => x.CatalogStandardId == item.CatalogStandardId
                                                                 && x.CustomerId == _userService.CustomerId
                                                                 && x.IsActive == true)
                                                        .SingleOrDefault();

            if (authenticateStandardItem != null)
                return true;
            else
                return false;
        }
    }
    public enum ItemType
    {
        QuoteId,
        CatalogId,
        StandardId
    }
    public class BasketAbstractService 
    {
        private readonly Func<BasketItemRequestModel, BasketAbstract> basketAbstract;

        //private readonly BasketAbstract basketAbstract;
        public BasketAbstractService(Func<BasketItemRequestModel, BasketAbstract> basketAbstract)
        {
            this.basketAbstract = basketAbstract;
        }

        public void chk(BasketItemRequestModel item)
        {
            try
            {

                //var chk = basketAbstract.AuthenticateItems(item);
                string name = basketAbstract.GetType().Name;
                string assembly = basketAbstract.GetType().Assembly.ToString();
                string fname = basketAbstract.GetType().FullName;
                string returntype = basketAbstract.Method.ReturnType.ToString();
                string type = basketAbstract.Method.ReflectedType.Name;
                string targettype = basketAbstract.Target.GetType().Name;
               
                
                
                
                var chk = basketAbstract(item).AuthenticateItems(item);


                string name1 = basketAbstract.GetType().Name;
                string assembly1 = basketAbstract.GetType().Assembly.ToString();
                string fname1 = basketAbstract.GetType().FullName;
                string returntype1 = basketAbstract.Method.ReturnType.ToString();
                string type1 = basketAbstract.Method.ReflectedType.Name;
                string targettype1 = basketAbstract.Target.GetType().Name;

            }
            catch (Exception ex)
            {


            }
            
            
        }

      
    }
}


 services.AddScoped<BasketAbstractService>();
            services.AddScoped<CatalogItem>();
            //services.AddScoped<BasketAbstract,CatalogItem>();
            //services.AddScoped<BasketAbstract, StandardItem>();


            services.AddScoped<Func<BasketItemRequestModel, BasketAbstract>>(serviceProvider => key =>
            {

                if (key.CatalogPartId != 0)
                {
                    return serviceProvider.GetService<CatalogItem>();
                }
                else
                {
                    return serviceProvider.GetService<CatalogItem>();
                }

            });
			
			
	    [HttpPost("/chk")]
        public IActionResult ckh(BasketItemRequestModel model)
        {
            service.chk(model);
            return Ok();
        }


