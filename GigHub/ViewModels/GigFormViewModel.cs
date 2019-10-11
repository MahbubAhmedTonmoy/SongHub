using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        public int Id { get; set; } // identify this is new(create Action) or old(update Action) -> 

        [Required] // for server side validation
        public string Venue { get; set; }

        [Required]
        [DateValidation]  // custom validation
        public string Date { get; set; }

        [Required]
        [TimeValidation] // custom validation 
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public string Heading { get; set; }// for heading of shared view

        public string Action // for heading action (create / edit) of shared view
        {
           
            get
            {
                //func<2 peremeter> delegate call anonymous method
                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(this));

                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create; // this is expression

                var actionName = (action.Body as MethodCallExpression).Method.Name; // extrate method name at run time

                return actionName;
                  
               // return (Id != 0) ? "Update": "Create"; // set id in Edit action 
            }
        }
        public IEnumerable<Genre> Geners{get;set;}

        public DateTime GetDateTime()
        {
              return DateTime.Parse(string.Format("{0} {1}", Date, Time)); 
        }
    }
}