using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using InternshipHMSModels.Models;
using InternshipHMSWeb.Controllers;
using InternshipHMSService.Core;
using InternshipHMSModels.ViewModels;
using InternshipHMSLibrary;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reservations_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            return Json(_unitOfWork.Reservation_List.GetAllForCustomerByDataSourceResult(request, id));
        }
        public ActionResult Customer_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Customer_List.GetAllByDataSourceResult(request));
        }
        public ActionResult Fellows_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            return Json(_unitOfWork.Customer_List.GetChildByDataSourceResult(request, id));
        }

        [HttpPost]
        public ActionResult Create(CustomerViewModel customer)
        {

            if (ModelState.IsValid)
            {
                if (!customer.CheckSubmitUserCustomer)
                {
                    var result = _unitOfWork.Customer_List.Add(CustomerMapper.Map(customer));
                    if (result.Succeeded)
                    {
                        Ok();
                        return RedirectToAction("Index", "Customer", null);
                    }
                }
                if (customer.CheckSubmitUserCustomer)
                {
                    var result = _unitOfWork.Customer_List.AddWithUser(CustomerMapper.Map(customer), customer.Password);
                    if (result.Succeeded)
                    {
                        Ok();
                        return RedirectToAction("Index", "Customer", null);
                    }
                }
            }
            Forbidden();
            return Content(GenerateError());
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel customer)
        {
           

            if (ModelState.IsValid)
            {
                switch (customer.EditMode)
                {
                    case CustomerEditState.UpdateCustomerAndUser:
                        _unitOfWork.Customer_List.UpdateWithUser(CustomerMapper.Map(customer));
                        this.Ok();
                        return RedirectToAction("Index", "Customer", null);
                    case CustomerEditState.UpdateCustomerCreateUser:
                        _unitOfWork.Customer_List.UpdateAndCreateUser(CustomerMapper.Map(customer), customer.Password);
                        this.Ok();
                        return RedirectToAction("Index", "Customer", null);
                    case CustomerEditState.UpdateCustomer:
                        _unitOfWork.Customer_List.Update(CustomerMapper.Map(customer));
                        this.Ok();
                        return RedirectToAction("Index", "Customer", null);
                    default:
                        break;
                }
            }

            Forbidden();
            return Content(GenerateError());
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.Customer_List.Delete(Guid.Parse(id));
                if (result.Succeeded)
                {
                    Ok();
                    return RedirectToAction("Index", "Customer", null);
                }
            }

            Forbidden();
            return Content(GenerateError());
        }

        public JsonResult Detail(string id)
        {

            InternshipHMSModels.Models.Customer customer = _unitOfWork.Customer_List.Find(Guid.Parse(id));
            return Json(new
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FullName = customer.FullName,
                NationalCode = customer.NationalCode,
                CustomerCode = customer.CustomerCode,
                PassportNumber = customer.PassportNumber,
                Birthdate = customer.Birthdate == null ? "" : $"{PersianDateConvertor.ConvertToPersianDate((DateTime)customer.Birthdate)} ({DateTime.Now.Year - customer.Birthdate.Value.Year})",
                Nationality = customer.Nationality,
                LivingPlace = customer.LivingPlace,
                ParentID = customer.ParentID == null ? "" : customer.ParentID.ToString(),
                ParentFullName = customer.ParentID == null ? "" : _unitOfWork.Customer_List.GetAll().FirstOrDefault(f => f.ID == customer.ParentID).FullName,
                Email = customer.Person_ApplicationUser == null ? "" : customer.Person_ApplicationUser.Email,
                LockoutEnabled = customer.Person_ApplicationUser == null ? "" : customer.Person_ApplicationUser.LockoutEnabled.ToString()
            }, JsonRequestBehavior.AllowGet);
        }



    }
}
