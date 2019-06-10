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
using InternshipHMSService.Core;
using InternshipHMSModels.ViewModels;
using InternshipHMSWeb.Controllers;
using InternshipHMSLibrary;
using Microsoft.AspNet.Identity;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class EmployeeController : BaseController
    {
        public EmployeeController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPass(ResetPassViewModel resetPass)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Person_List.ResetPass(resetPass);
                Ok();
                return RedirectToAction("Index", "Employee", null);
            }
            Forbidden();
            return Content(GenerateError());
        }

        public ActionResult People_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Employee_List.GetAllByDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.CreateUser)
                    _unitOfWork.Employee_List.AddWithUser(EmployeeMapper.Map(employee),
                        _unitOfWork.Person_List.GetRoleNameForUserId(User.Identity.GetUserId()),
                        employee.Password);
                else
                    _unitOfWork.Employee_List.Add(EmployeeMapper.Map(employee));
                Ok();
                return RedirectToAction("Index", "Employee", null);
            }
            Forbidden();
            return Content(GenerateError());

        }

        public JsonResult Detail(string id)
        {
            Employee employee = _unitOfWork.Employee_List.Find(Guid.Parse(id));
            return Json(new
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                NationalCode = employee.NationalCode,
                EmployeeCode = employee.EmployeeCode,
                Email = employee.Person_ApplicationUser == null ? "" : employee.Person_ApplicationUser.Email,
                Role = employee.RoleHelper == null ? "" : Translator.RoleTranslateToName(employee.RoleHelper),
                Lock = employee.Person_ApplicationUser == null ? "" : employee.Person_ApplicationUser.LockoutEnabled.ToString()
                // برای ارسال دسترسی عملیات ترجه از سمت سرور برای امنیت انجام شده
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                switch (employee.EditMode)
                {
                    case EditState.UpdateEmployeeAndUser:
                        _unitOfWork.Employee_List.UpdateWithUser(EmployeeMapper.Map(employee),
                            _unitOfWork.Person_List.GetRoleNameForUserId(User.Identity.GetUserId()));
                        this.Ok();
                        return RedirectToAction("Index", "Employee", null);
                    case EditState.UpdateEmployeeCreateUser:
                        _unitOfWork.Employee_List.UpdateAndCreateUser(EmployeeMapper.Map(employee),
                            _unitOfWork.Person_List.GetRoleNameForUserId(User.Identity.GetUserId()), employee.Password);
                        this.Ok();
                        return RedirectToAction("Index", "Employee", null);
                    case EditState.UpdateEmployee:
                        _unitOfWork.Employee_List.Update(EmployeeMapper.Map(employee));
                        this.Ok();
                        return RedirectToAction("Index", "Employee", null);
                    default:
                        break;
                }
            }

            Forbidden();
            return Content(GenerateError());

        }

        public ActionResult UserEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = _unitOfWork.Employee_List.GetAll().FirstOrDefault(f => f.Person_ApplicationUser.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "ID,FirstName,LastName,NationalCode,Birthdate,Nationality,LivingPlace,CreateDate,EmployeeCode")] Employee employee)
        {
            _unitOfWork.Employee_List.Update(employee);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Home");
        }


    }
}
