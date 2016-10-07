using System;
using System.Collections.Generic;
using Nancy;

namespace GNG
{
    public class GNGModule : NancyModule
    {
        public GNGModule()
        {
            Get("/", args =>
            {
                ViewBag.tooHigh = false;
                ViewBag.tooLow = false;
                ViewBag.correct = false;
                ViewBag.showForm = true;
 
                if(Session["num"] == null)
                {
                    int num = new Random().Next(1,101);
                    Session["num"] = num;
                    Console.WriteLine("The random # is: " + num);
                }

                return View["GNG.sshtml"];
            });

            Post("/process", args =>
            {
                int guess = Request.Form["guess"];
                int num = (int) Session["num"];
                
                if(guess < num)
                {
                    ViewBag.tooLow = true;
                    ViewBag.showForm = true;
                    Console.WriteLine("too low");
                    return View["GNG.sshtml"];
                }
                else if(guess > num)
                {
                    ViewBag.tooHigh = true;
                    ViewBag.showForm = true;
                    Console.WriteLine("too high");
                    return View["GNG.sshtml"];
                }
                else if(guess == num)
                {
                    ViewBag.correct = true;
                    ViewBag.showForm = false;
                    Console.WriteLine("correct");
                    return View["GNG.sshtml"];
                }
                return Response.AsRedirect("/");
            });

            Post("/clear", args =>
            {
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });
        }
    }
}
