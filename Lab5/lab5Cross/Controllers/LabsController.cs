using All_Labs;
using lab5Cross.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5Cross.Controllers
{
    [Authorize]
    public class LabsController : Controller
    {

        public IActionResult Lab1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Lab1(DataModel model)
        {
            try
            {
                string[] RefiendData = model.Data[0].Split("\r\n");
                model.Response = PR1.Main(RefiendData);
                model.Calculated = true;
                Console.WriteLine(model);
            }
            catch(ArgumentException aex)
            {
                model.ErrorValue = aex.Message;
            }
            catch(Exception ex)
            {
                model.ErrorValue = ex.Message;
            }
            
            return View(model);
        }
        public IActionResult Lab2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Lab2(DataModel model)
        {
            try
            {
                string[] RefiendData = model.Data[0].Split("\r\n");
                model.Response = PR2.Main(RefiendData);
                model.Calculated = true;
                Console.WriteLine(model);
            }
            catch (ArgumentException aex)
            {
                model.ErrorValue = aex.Message;
            }
            catch (Exception ex)
            {
                model.ErrorValue = ex.Message;
            }

            return View(model);
        }
        public IActionResult Lab3()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Lab3(DataModel model)
        {
            try
            {
                string[] RefiendData = model.Data[0].Split("\r\n");
                model.Response = PR3.Main(RefiendData);
                model.Calculated = true;
                Console.WriteLine(model);
            }
            catch (ArgumentException aex)
            {
                model.ErrorValue = aex.Message;
            }
            catch (Exception ex)
            {
                model.ErrorValue = ex.Message;
            }

            return View(model);
        }

    }
}
